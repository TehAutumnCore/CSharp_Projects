using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
<<<<<<< HEAD
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
=======
builder.Services.AddDbContext<BlogDbContext>(options => options.UseInMemoryDatabase("BlogDb"));
>>>>>>> b1953dcec53398302c428abd09f6ed853a410c37

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
