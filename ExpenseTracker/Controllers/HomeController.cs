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
        var allExpenses = _context.Expenses.ToList();//ToList() executes the query

        ViewBag.Expenses = allExpenses.Sum(x => x.Value); //we take all of the expenses and get the sum of them
        //  ViewBag.Expenses = totalExpenses;
        
        return View(allExpenses);
    }
    public IActionResult CreateEditExpense(int? id) 
    {
        //editing -> loading an expense by id
        if (id != null) {         //SingleOrDefault grab the first expense id with the id we're looking for
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expenseInDb);
        }
        return View();
    }

    public IActionResult DeleteExpense(int id) {
        
        var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
        _context.Expenses.Remove(expenseInDb);
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }
    
    public IActionResult CreateEditExpenseForm(Expense model)
    {
        if(model.Id == 0) {
            //Create 
            _context.Expenses.Add(model); //DbContext.ExpensesTable(class).Add(model)
        } 
        else 
        {
            _context.Expenses.Update(model);
        }
        //_context references DB, Expenses table and adds the model
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
