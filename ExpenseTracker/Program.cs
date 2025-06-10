using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data; //Data is the name of directory

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Registers ExpenseTrackerDbContext with the app's dependency injection system and tells Entity Framework Core to use an in-memory database called ExpenseTrackerDB
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options => 
    options.UseInMemoryDatabase("ExpenseTrackerDB"));


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
