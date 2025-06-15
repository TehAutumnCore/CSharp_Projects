using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Components.Web;

namespace Blog.Controllers.Mvc;

public class ProjectsController : Controller
{
    private readonly List<Project> _allProjects = new()
    {
        new Project {Title = "Portfolio Blog", Description = "Tech Stack : C#, ASP.NET Core, Entity Framework, SQL Server, JWT, Razor Views\n Description: Full-stack blog application with separate MVC and REST API layers. Features role-based authentication, session-based JWT, admin-only CRUD, responsive Bootstrap UI, accessibility (WCAG), dark mode, and contact form with secure SMTP email sending.", Link="https://github.com/TehAutumnCore/CSharp_Projects/tree/main/Blog"},

        new Project {Title = "RedZone Getaway", Description = "Tech Stack : Python, JavaScript, React, Django, PostgreSQL\n Description: RedZone Getaway Serves as the ultimate platform for NFL enthusiasts, offering a seamless blend of real-time stats, travel planning, and fan engagement. By Integrating sports and travel, RedZone Getaway delivers an unmatched experience for football fans.", Link="https://github.com/Samue1eun/RedzoneGetaway"},

        new Project {Title = "Game Management", Description = "Tech Stack : Python, JavaScript, React, Django, PostgreSQL\n Description: Solo full-stack project using Twitch and Steam APIs. Offers game discovery, live streams, and CRUD functionality. Features secure token-based authentication and a custom Netflix-style UI for managing game favorites.", Link="https://github.com/TehAutumnCore/Yankee_Platoon_Assignments/tree/main/12-Personal-Project/GameManagementPersonalProject"},

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