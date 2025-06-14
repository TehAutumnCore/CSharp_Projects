using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Components.Web;

namespace Blog.Controllers.Mvc;

public class ProjectsController : Controller
{
    private readonly List<Project> _allProjects = new()
    {
        new Project {Title = "Name of App Here", Description = "App description here", Link="https://github.com/TehAutumnCore/"},
        new Project {Title = "Name of App Here", Description = "App description here", Link="https://github.com/TehAutumnCore/"},
        new Project {Title = "Name of App Here", Description = "App description here", Link="https://github.com/TehAutumnCore/"},
        new Project {Title = "Name of App Here", Description = "App description here", Link="https://github.com/TehAutumnCore/"},
    };

    public IActionResult Index(int page = 1)
    {
        const int pageSize = 4; //4 projects to a page
        var totalProjects = _allProjects.Count; //total is how many is inside the db
        var totalPages = (int)Math.Ceiling(totalProjects / (double)pageSize); //getting the total pages by rounding up from total of projects / how many projects per page

        var pagedProjects = _allProjects //grab all projects from the db
        .Skip((page - 1) * pageSize) //skip the first 0
        .Take(pageSize) //take the next 4
        .ToList(); //render through list

        var viewModel = new ProjectIndexViewModel //map the viewModel to the ProjectIndexViewModel properties
        {
            Projects = pagedProjects, //Projects = _allProjects
            CurrentPage = page,// 
            TotalPages = totalPages
        };

        return View(viewModel); //return the view/page with 4 projects
    }
}