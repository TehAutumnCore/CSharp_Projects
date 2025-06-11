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

    [HttpGet]
    public IActionResult Index() //All blogPosts
    {
        var blogPosts = _context.Blogs
        .OrderByDescending(p => p.Date) //view by date so the second post dated yesterday wont be after the first post dated today, OrderBy() to flip order
        .ToList(); //Renders a list of all blogs
        return View(blogPosts);
    }
    
    [HttpGet] //default behaviour for get
    public IActionResult Create() //returns Create.cshtml and show the form
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

    //Edit - Http Get Action (check if post id exist)
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var post = _context.Blogs.Find(id); //find the post id, .SingleOrDefault(post=>Id == id) if more complex conditions and slower->builds query for lookups
        if (post == null) return NotFound(); //if post id doesnt exist //NotFound will produce a Status404NotFound response
        return View(post); //return the Edit.cshtml View 
    }

    [HttpPost] //Http Post Action
    [ValidateAntiForgeryToken] //Validates the AntiForgeryToken, if its not avaiable or is invalid, the validation will fail and the action method will not execute
    public IActionResult Edit(int id, BlogPost blogPost)
    {
        if (id != blogPost.Id)
        {
            return BadRequest(); //Creates an http status error 400 bad request
        }

        if (ModelState.IsValid) //Check if model passed rules set in model such as [Required]
        {
            _context.Blogs.Update(blogPost);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(blogPost);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var post = _context.Blogs.Find(id);
        return (post == null) ? NotFound() : View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var post = _context.Blogs.Find(id);
        if (post == null) return NotFound();

        _context.Blogs.Remove(post);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));

    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}