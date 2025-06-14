using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using Blog.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blog.Controllers.Mvc;

public class BlogViewController : Controller
{
    private readonly BlogDbContext _context;

    public BlogViewController(BlogDbContext context)
    {
        _context = context;
    }
    /* Pre Pagination Logic
    public IActionResult Index()
    {
        var posts = _context.Blogs.OrderByDescending(p => p.Date).ToList();
        return View(posts);
    }
    */

    //Index with Pagination
    public IActionResult Index(int pages = 1) //default to page number 1 if not provided
    {
        const int pageSize = 5; //show 5 post per page

        var totalPosts = _context.Blogs.Count(); //count how many posts are in the db.Blogs table
        var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize); //divide the total posts for ex: 6 / 5 = 1.2 (rounded up will equal 2), so 5 on first page, 1 on 2nd page.

        //Page 1 -> Skip 0 Shows the first 5          Skip ((1-1) * 5) = 0 Elements, take 5
        //Page 2 -> Skips the first 5 Shows 6-10      Skip ((2-1) * 5) = 5 Elements, take 5
        //page 3 -> Skips the first 10, shows 11-15   Skip ((3-1) * 5) = 10 Elements, take 5
        var posts = _context.Blogs //all posts in db.Blogs table
        .OrderByDescending(p => p.Date) //descending order by date, so newest post comes first
        .Skip((pages - 1) * pageSize) //skips specified number of elements and returns the remaining elements, Skip previouus page(s) posts
        .Take(pageSize) //returns the number of contiguous elements from the start of a sequence, Take this page's 5 posts
        .ToList(); //convert result to List to render

        //packages all of the posts and paging info into a BlogIndexViewModel ViewModel
        var viewModel = new BlogIndexViewModel
        {
            BlogPosts = posts,
            CurrentPage = pages,
            TotalPages = totalPages
        };
        return View(viewModel); //sends ViewModel to Index.cshtml
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