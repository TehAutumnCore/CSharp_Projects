using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blog.Controllers.Mvc;

public class BlogViewController : Controller
{
    private readonly BlogDbContext _context;

    public BlogViewController(BlogDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var posts = _context.Blogs.OrderByDescending(p => p.Date).ToList();
        return View(posts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(); //Views/Blog/Create.cshtml
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BlogPost post)
    {
        if (ModelState.IsValid)
        {
            _context.Blogs.Add(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var post = _context.Blogs.Find(id);
        if (post == null) return NotFound();
        return View(post); //Views/Blog/Edit.cshtml
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, BlogPost post)
    {
        if (id != post.Id) return BadRequest(); //400 Bad request
        if (!ModelState.IsValid) return View(post);

        _context.Blogs.Update(post);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var post = _context.Blogs.Find(id);
        if (post == null) return NotFound();
        return View(post);
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
}