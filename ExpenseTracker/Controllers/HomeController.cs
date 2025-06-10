using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ExpenseTrackerDbContext _context; //references the injected ExpenseTrackerDbContext

    public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbContext context) //ASP.NET Core's DependencyInjection system automatcially creates and injects when constructor is called 
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() //Looks for Views/Home/Index.cshtml
    {
        return View();
    }

    public IActionResult Privacy() //Looks for Views/Home/Index.cshtml
    {
        return View();
    }
    public IActionResult Expenses()
    {
        return View();
    }
    public IActionResult CreateEditExpense()
    {
        return View();
    }
    
    public IActionResult CreateEditExpenseForm(Expense model)
    {
        //_context references DB, Expenses table and adds the model
        _context.Expenses.Add(model);
        _context.SaveChanges(); //have to update db anytime you edit the db, add,remove, edit etc
        // After form is submitted will redirect to Expenses page to see result
        return RedirectToAction("Expenses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
