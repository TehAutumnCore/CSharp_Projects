# Blog App - ASP.NET Core MVC + REST API

This project is a full-stack blog application built with **ASP.NET Core 8.0**, combining both a modern RESTful API and a classic MVC Razor frontend.

It is designed for full CRUD functionality, accessibility, and scalable architecture using best practices like DTOs, separation of concerns, and frontend/backend separation.

## ✨ Features

- ✅ **RESTful API** for blog posts (CRUD)
- ✅ **MVC Razor Views** for frontend UI
- ✅ **DTOs** for safe, client-facing API communication
- ✅ **Separate controllers** for API and MVC
- ✅ **Entity Framework Core** (SQL Server or In-Memory)
- ✅ **Pagination** in MVC list view
- ✅ **Dark mode toggle** (🌙 / ☀️, with localStorage)
- ✅ **Accessibility improvements** (WCAG skip link, semantic landmarks)
- ✅ **Bootstrap-based styling**

## 🗂️ Project Structure

```
/Controllers
  /Api/BlogController.cs       --> RESTful API controller
  /Mvc/BlogViewController.cs   --> MVC Razor UI controller

/Views/BlogView/               --> Razor pages (Index, Create, Edit, Delete)
/DTOs/                         --> BlogPostDto for API communication
/ViewModels/                   --> BlogIndexViewModel (for pagination)
/Models/                       --> BlogPost model (for EF Core)
/Data/                         --> BlogDbContext database context
/wwwroot/css/                  --> site.css (dark mode, responsive styles)
/wwwroot/js/                   --> dark-mode-toggle.js
```

## 📦 Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core (Code First)
- MVC Razor Pages
- REST API (ControllerBase)
- Bootstrap 5
- Postman (for API testing)
- C# .NET SDK 8.0.4+
- localStorage (theme persistence)

## 🚀 API Endpoints

| Method | Endpoint          | Description         |
|--------|-------------------|---------------------|
| GET    | /api/blog         | Get all blog posts  |
| GET    | /api/blog/{id}    | Get post by ID      |
| POST   | /api/blog         | Create new post     |
| PUT    | /api/blog/{id}    | Update existing post|
| DELETE | /api/blog/{id}    | Delete post         |

## 🖥️ Frontend (MVC Views)

| Route                    | Description            |
|--------------------------|------------------------|
| /BlogView                | List all posts         |
| /BlogView/Create         | Create new post        |
| /BlogView/Edit/{id}      | Edit existing post     |
| /BlogView/Delete/{id}    | Confirm and delete     |

### ♿ Accessibility

- Semantic HTML tags: `<header>`, `<main>`, `<footer>`
- ARIA attributes for better screen reader support
- "Skip to content" link
- High-contrast dark mode

### 🌗 Dark Mode

- Toggleable via button in navbar
- Automatically persists via `localStorage`
- Accessible via keyboard
- Uses custom styles + Bootstrap

## 🛠️ Setup Instructions

```bash
git clone <your-repo-url>
cd Blog
dotnet restore
dotnet dev-certs https --trust
dotnet run
```

Then navigate to:

- `https://localhost:{port}/BlogView` — for UI
- `https://localhost:{port}/api/blog` — for API (test via Postman)

## 🔐 Coming Next

- JWT-based user authentication
- Login, logout, and signup with secure tokens
- Role-based authorization for admin/user views
- API `[Authorize]` protection
- Deployment to Azure

---

**Author:** Gary Rojas


## 🔐 Developer Notes: Managing Secrets Locally
Dotnet user-secrets system writes to a secrets store on your local machine, not into the project. Its a per-user file stored securely on Linux ~/.microsoft/usersecrets/{guid}/secrets.json


This app uses `dotnet user-secrets` to manage sensitive configuration during development:

- The JWT signing key is stored securely using:
dotnet user-secrets set "Jwt:Key" "your-secret-key" - To view current secrets:
dotnet user-secrets list Secrets are not stored in `appsettings.json` and are not checked into source control.
