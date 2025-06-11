using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;

namespace Blog.Controllers;

public class BlogController : Controller
{
    private readonly BlogDbContext _context;

    public BlogController(BlogDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var blogPosts = _context.Blogs
        .OrderByDescending(p => p.Date) //view by date so the second post dated yesterday wont be after the first post dated today, OrderBy() to flip order
        .ToList(); //Renders a list of all blogs
        return View(blogPosts);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost] //tells aspnet mvc that the action method should only respond to HTTP Post requests
    [ValidateAntiForgeryToken] //Will create a hidden form field with a token and when submitted will check if it matches what the server expects
    public IActionResult Create(BlogPost post) //Create - Pass post<BlogPost> model
    {
        if (ModelState.IsValid) //Checks if the model passed the validation rules such as [Required in the model class]
        {
            _context.Blogs.Add(post); //Add the post to the Db
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); //returns the string "Index" same and safer way of RedirectToAction("Index")
        }
        return View(post); //re-show form with validation errors
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}