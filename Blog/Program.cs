using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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

app.UseAuthentication(); //Jwt auth - must authenticate before authorizing
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
