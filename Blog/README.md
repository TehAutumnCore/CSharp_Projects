# Blog App - ASP.NET Core MVC + REST API

This project is a full-stack blog application built with ASP.NET Core 8.0. It demonstrates both a RESTful API using `ControllerBase` and a traditional MVC frontend using Razor Views.

## ✨ Features

- ✅ RESTful API for blog posts (CRUD)
- ✅ Razor Views for user-friendly frontend (MVC pattern)
- ✅ DTOs for safe data transfer in the API
- ✅ Separate API and MVC controller architecture
- ✅ Entity Framework Core with SQL Server (or in-memory DB)
- ✅ Bootstrap-ready UI components (optional)

## 🗂️ Project Structure

```
/Controllers
  /Api/BlogController.cs       --> RESTful API for BlogPost
  /Mvc/BlogViewController.cs   --> Razor UI logic
/Views/BlogView/               --> Razor views (Index, Create, Edit, Delete)
/DTOs/                         --> BlogPostDto (for API only)
/Models/                       --> BlogPost model
/Data/                         --> BlogDbContext for EF Core
/wwwroot/                      --> Static files (CSS/JS)
```

## 📦 Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- Razor Pages (MVC)
- REST API + Postman testing
- Bootstrap 5 (optional UI polish)
- C# .NET SDK 8.0.4+

## 🚀 API Endpoints

| Method | Endpoint          | Description         |
|--------|-------------------|---------------------|
| GET    | /api/blog         | Get all blog posts  |
| GET    | /api/blog/{id}    | Get post by ID      |
| POST   | /api/blog         | Create a new post   |
| PUT    | /api/blog/{id}    | Update post         |
| DELETE | /api/blog/{id}    | Delete post         |

## 🖥️ Frontend (MVC Views)

| Route             | Description            |
|------------------|------------------------|
| /BlogView         | List all posts         |
| /BlogView/Create  | Create new post        |
| /BlogView/Edit/{id} | Edit existing post   |
| /BlogView/Delete/{id} | Confirm delete     |

## 🛠️ Setup Instructions

```bash
git clone <your-repo-url>
cd Blog
dotnet restore
dotnet dev-certs https --trust
dotnet run
```

Navigate to `https://localhost:{port}/BlogView` to view the UI.

## 🔐 Coming Next

- JWT-based user login and authentication
- Role-based authorization
- API protection with [Authorize]
- Deployment to Azure or Render

---

**Author:** Your Name  
**License:** MIT  
