# Blog Portfolio — ASP.NET Core 8.0 Full-Stack Application

This is a fully integrated **personal portfolio and blogging platform** built with ASP.NET Core 8.0. It includes both a **RESTful API** for backend logic and a **Razor Pages UI** for user interaction, designed to showcase projects, share blog posts, and allow users to securely contact the developer.

---

## ✨ Features

- ✅ **MVC Razor Views** (Home, Projects, Blog, Contact)
- ✅ **RESTful API** for full blog post CRUD
- ✅ **JWT Authentication & Role-based Authorization**
- ✅ **Session-based login/logout in MVC with secure JWT flow**
- ✅ **Separate controllers for API and MVC**
- ✅ **DTOs** for safe data exposure
- ✅ **Pagination** for blogs/projects
- ✅ **Responsive, accessible UI** (ARIA, WCAG, skip links)
- ✅ **Dark mode toggle** (🌙 / ☀️ with localStorage)
- ✅ **Contact form** with real email delivery via SMTP (Gmail)

---

## 🗂️ Project Structure

```
/Controllers
  /Api/BlogController.cs        --> RESTful API controller
  /Mvc/BlogViewController.cs    --> Razor blog UI
  /Mvc/ProjectsController.cs    --> Static portfolio projects
  /Mvc/ContactController.cs     --> Contact form + email

/Models                         --> BlogPost, Project, ContactFormModel
/DTOs                           --> LoginDTO, RegisterDTO, BlogPostDTO
/ViewModels                     --> BlogIndexViewModel, ProjectIndexViewModel
/Views                          --> Razor views for BlogView, Home, Projects, Contact
/Data                           --> PortfolioDbContext (In-Memory or SQL Server)
/Services                       --> JwtTokenService for token generation

/wwwroot/css/site.css           --> Custom styles (dark mode, layout)
/wwwroot/js/dark-mode-toggle.js --> Theme toggle
```

---

## 🧰 Tech Stack

- **Frontend**: ASP.NET Core MVC Razor, Bootstrap 5, JavaScript
- **Backend**: ASP.NET Core Web API, Entity Framework Core
- **Authentication**: JWT, role-based `[Authorize]`
- **Email Service**: SMTP with Gmail & .NET MailClient
- **Database**: EF Core In-Memory or SQL Server (Code First)
- **Tools**: Postman, Visual Studio Code, dotnet CLI

---

## 📬 Contact Form

Users can contact you directly via a custom contact form powered by:

- Secure server-side validation
- `SmtpClient` email delivery using Gmail
- Secrets stored securely with `dotnet user-secrets`
- Supports Post/Redirect/Get pattern

---

## 📁 API Endpoints

| Method | Endpoint          | Description             |
|--------|-------------------|-------------------------|
| GET    | /api/blog         | Get all blog posts      |
| GET    | /api/blog/{id}    | Get post by ID          |
| POST   | /api/blog         | Create new post (Admin) |
| PUT    | /api/blog/{id}    | Update post (Admin)     |
| DELETE | /api/blog/{id}    | Delete post (Admin)     |

---

## 🖥️ Razor UI Routes

| Route               | Description              |
|---------------------|--------------------------|
| /                  | Home with bio + resume   |
| /BlogView           | Blog post list view      |
| /BlogView/Create    | Create blog post (Admin) |
| /Projects           | Static project showcase  |
| /Contact            | Contact form with email  |

---

## ♿ Accessibility Highlights

- Skip links & ARIA roles
- Keyboard-friendly navigation
- Dark mode with contrast-tested theme

---

## 🌐 Setup Instructions

```bash
git clone https://github.com/TehAutumnCore/CSharp_Projects.git
cd Blog
dotnet restore
dotnet dev-certs https --trust
dotnet run
```

Then visit:

- `https://localhost:{port}/BlogView`
- `https://localhost:{port}/api/blog`
- `https://localhost:{port}/Contact`

---

## 🔐 Managing Secrets

Uses `dotnet user-secrets` for configuration:

```bash
dotnet user-secrets set "Jwt:Key" "your-secret-key"
dotnet user-secrets set "EmailSettings:SenderEmail" "you@gmail.com"
dotnet user-secrets set "EmailSettings:SmtpPassword" "your-app-password"
```

See all secrets:
```bash
dotnet user-secrets list
```

---

## 🚀 Under Consideration

- Full deployment on Azure App Service
- Persistent DB (SQL Server/Azure)
- Frontend enhancements with Blazor or Vue

---

**Author**: Gary Rojas  
[GitHub](https://github.com/TehAutumnCore) | [LinkedIn](https://linkedin.com/in/gary-rojas-647248160/)  
📧 Rojas.Gary.J@outlook.com
