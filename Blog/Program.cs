using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Blog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); //needed to provide HttpClient isntances via dependency Injection
builder.Services.AddDbContext<PortfolioDbContext>(options => options.UseInMemoryDatabase("BlogDb"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>(); //When somethings asks for IJwtTokenService give it an instance of JwtTokenService
//JWT Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Registers the authentication middleware to let it know we're using JWT Bearer token authentication
.AddJwtBearer(options => //in appsettings.json or user-secrets and
{
    var key = builder.Configuration["Jwt:key"]; //load the signing key
    options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuer = false,//Dont need to verify who issued the token
        ValidateAudience = false,//Dont need to verify who it was intended for
        ValidateIssuerSigningKey = true, //DO need to verify JWT was signed with the correct secret
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)) //to ensure the token was not tampered with
    };
});
builder.Services.AddAuthorization(); //Enables [Authorize] attributes to be respected
builder.Services.AddSession(options => //enables HttpContext.Session
{                                                   //configures the following
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set how long the session lasts
    options.Cookie.HttpOnly = true;                 // Prevent JS Access
    options.Cookie.IsEssential = true;              // Required for GDPR compliance -- a rule set to protect personal data and privaacy of EU individuals
});

var app = builder.Build(); 

//admin seeding logic so I, Gary am the sole Admin Creator of all things....related here...user-secrets is pretty cool :)
using (var scope = app.Services.CreateScope()) //Creates a temporary DI scope, since we're not inside a controller or service class to manually grab IConfiguration, and PortfolioDbContext
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>(); // so you can read from appsettings.json, user-secrets, environmental variables, etc.
    var context = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();// grabs database context from DI, to query or update the db manually.

    var adminUsername = config["AdminCredentials:Username"]; //grabs stored secrets from user-secrets for Admin Username
    var adminPassword = config["AdminCredentials:Password"];//grab stored secrets from user-secrets for Admin Password

    if (!context.Users.Any(u => u.Username == adminUsername)) //checks if username already exists in the database
    {
        using var hmac = new HMACSHA512(); //creates HMACSHA512 Hasher to generate password salt(hmac.Key) and hash the password
        var adminUser = new AppUser //Creates new AppUser object with Username from secret store with admin and secured hashed password
        {
            Username = adminUsername!, //! is a null forgiving operator because compiler cant guarantee its not null
            Role = "Admin",
            PasswordSalt = hmac.Key,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword!)) //hashes password with salt(hmac.Key)
        };

        context.Users.Add(adminUser); //adds the admin user to teh database and saves it permanently
        context.SaveChanges(); //save database changes
    }
}

// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); //Adds session middleware to the request pipeline
app.Use(async (context, next) => //when a JWT token is stored in session treat the user as authenticated using this token
{
    var token = context.Session.GetString("JwtToken"); 
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Authorization = $"Bearer {token}";
    }
    await next();
});

app.UseAuthentication(); //Jwt auth - must authenticate before authorizing
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
