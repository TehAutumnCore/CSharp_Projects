using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using Blog.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components.Web;

namespace Blog.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase //ControllerBase is for building RESTful APIs while Controller is specifically for handling web pages and views
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
        .Select(post => new BlogPostDto //Transforms one type into another like a map in js or py
        {
            Title = post.Title,
            Description = post.Description,
            Author = post.Author,
            Date = post.Date
        })
        .ToList(); //Renders a list of all blogs
        // return View(blogPosts);
        return Ok(blogPosts);
    }


    /* Removed because RESTful APIs/ControllerBase does not support View();
    [HttpGet] //default behaviour for get
    public IActionResult Create() //returns Create.cshtml and show the form
    {
        return View();
    }
    */

    [HttpGet("{id}")]
    public IActionResult GetPostById(int id) //Grab individual Post by id
    {
        var post = _context.Blogs.Find(id); //Look for id
        if (post == null) return NotFound(); //if post doesnt exist return NotFound

        var dto = new BlogPostDto
        {
            Title = post.Title,
            Description = post.Description,
            Author = post.Author,
            Date = post.Date
        };

        return Ok(dto);
        //else return the post with status 200 OK
    }

    [HttpPost] //tells aspnet mvc that the action method should only respond to HTTP Post requests
    // [ValidateAntiForgeryToken] //Will create a hidden form field with a token and when submitted will check if it matches what the server expects
    public IActionResult Create([FromBody] BlogPostDto dto) //Create - Pass post<BlogPost> model, Takes JSON from the request body and maps to C# object
    {   //Checks if the model doesnt passed the validation rules such as [Required in the model class]
        if (!ModelState.IsValid) return BadRequest(ModelState); //error 400 + validation errors

        var post = new BlogPost
        {
            Title = dto.Title,
            Description = dto.Description,
            Author = dto.Author,
            Date = dto.Date
        };
        /* JSON Format
            Headers: 
            Key -> Content-Type -> Tells the server whgat format the request body is in
            Value -> application/json specifies that you're spending JSON data.
            {
              "title": "Using DTOs",
              "description": "Testing DTO conversion",
              "author": "Gary",
              "date": "2025-06-11"
            }
        */
        _context.Blogs.Add(post); //Add the post to the Db
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post); //Returns HTTP 201 
        /* MVC portion
        // Created, and includes a location header with the post's URL
        // return RedirectToAction(nameof(Index)); //returns the string "Index" same and safer way of RedirectToAction("Index")
        return View(post); //re-show form with validation errors
        */
    }
    /*JSON Example ^
    {
      "title": "Learning REST",
      "description": "This is my post",
      "author": "Gary",
      "date": "2025-06-10"
    }
    */

    /* Replacing GET + POST with a put API
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
    */
    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] BlogPostDto dto)
    {
        // if (id != blogpost.Id) return BadRequest("ID mismatch"); //if ID is out of range or doesnt exist return BadRequest
        if (!ModelState.IsValid) return BadRequest(ModelState); //if Model doesnt pass validation checks in Model, return BadRequest and that Model
        var existingPost = _context.Blogs.Find(id);//check if post exist by id
        if (existingPost == null) return NotFound(); //if not, return 404, NotFound()

        //Updating the only allowed fields
        existingPost.Title = dto.Title;
        existingPost.Description = dto.Description;
        existingPost.Author = dto.Author;
        existingPost.Date = dto.Date;

        _context.SaveChanges();
        return NoContent(); //Http status code 204, no content

    }
    /* Replacing Delete with Delete API
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
    */
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var post = _context.Blogs.Find(id); //locate post by id
        if (post == null) return NotFound(); //return 404 Not found if post doesnt exist

        _context.Blogs.Remove(post); //remove post from Blogs table in db
        _context.SaveChanges(); //save db  changes
        return NoContent(); //204 no content
    }

    /* IAction Error() Only for MVC Views, Not APIs. ASPNET will return JSON Error Responses automatically for unhandled exceptions
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    */

}