# 📄 ResumeMatcher

**ResumeMatcher** is a full-stack web application that lets users upload their resumes and compares them to job descriptions from a dataset. The system analyzes keyword alignment, skill gaps, and produces visual insights using interactive charts and optional Power BI integration.

![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=flat&logo=dotnet&logoColor=white)
![Python](https://img.shields.io/badge/Python-3.x-yellow?style=flat&logo=python&logoColor=blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=flat&logo=postgresql&logoColor=white)

---

## 🔍 Purpose

This project is designed to:
- Help job seekers understand how well their resumes align with real-world job descriptions
- Provide visual feedback on missing or overlapping skills
- Showcase a clean integration of ASP.NET Core, Python, PostgreSQL, and data visualization

---

## 🧰 Tech Stack

| Layer            | Technology              | Purpose                                     |
|------------------|--------------------------|---------------------------------------------|
| Backend API      | ASP.NET Core 8 (Web API) | Resume upload, endpoints, DB operations     |
| Analytics Engine | Python 3 (`spaCy`, `pandas`) | Keyword extraction, skill matching      |
| Database         | PostgreSQL               | Store resumes, jobs, keyword mappings       |
| Visualization    | Chart.js (or Power BI)   | Skill match score, missing keywords         |
| File Storage     | Local (`/uploads/`)      | Store user-submitted resumes                |
| DevOps           | Git + GitHub             | Version control and collaboration           |
| OS/Platform      | Ubuntu (WSL compatible)  | Development environment                     |

---

## 📦 Features

- ✅ Upload resume (PDF or TXT)
- ✅ Parse and extract skills/keywords
- ✅ Compare against real job descriptions
- ✅ Display skill match percentage
- ✅ Show visual breakdown (pie/bar charts)
- ✅ Modular and maintainable architecture

---

## 🔁 Application Flow

1. **User uploads resume**
2. **ASP.NET Core API** receives file → saves to `/uploads`
3. **Python script** parses and extracts keywords
4. **PostgreSQL** stores jobs and parsed resume keywords
5. **Skill match** and missing keywords are calculated
6. **API** returns result to frontend
7. **Frontend** displays charts (Chart.js or Power BI)

---

## 📁 Folder Structure

```
ResumeMatcher/
│
├── ResumeMatcher.sln                 # Solution file
├── .gitignore                        # Covers .NET, Python, uploads, etc.
├── README.md                         # Clear purpose, flow, setup, features
├── LICENSE                           # MIT License added
│
├── ResumeMatcher.API/                # ASP.NET Core Web API
│   ├── Controllers/                  # API endpoints (e.g., UploadController)
│   ├── Models/                       # Resume, JobDescription, etc.
│   ├── DTOs/                         # Strongly-typed transfer objects
│   ├── Data/                         # DbContext + EF Core Migrations
│   ├── Services/                     # Business logic layer
│   └── Program.cs, appsettings.json  # Entrypoint and config
│
├── ResumeMatcher.Analytics/          # Python keyword matching & NLP
│   ├── resume_parser.py              # Extract text from uploaded resumes
│   ├── matcher.py                    # Compare skills with jobs
│   └── tokenizer.py                  # Clean/tokenize text (if needed)
│
├── datasets/                         # Real job descriptions (CSV, JSON)
├── uploads/                          # Temporarily holds uploaded resumes
├── scripts/                          # SQL seed scripts or helper tools

```

---

## ⚙️ Setup Instructions (Ubuntu/WSL2)

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/ResumeMatcher.git
cd ResumeMatcher
```

### 2. Install dependencies

#### .NET Backend
- Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Navigate to API folder and run:

```bash
cd ResumeMatcher.API
dotnet restore
dotnet run
```

#### Python (Analytics Engine)

```bash
cd ResumeMatcher.Analytics
python3 -m venv venv
source venv/bin/activate
pip install -r requirements.txt
```

**Sample `requirements.txt`:**
```
pandas
spacy
python-docx
pdfminer.six
```

Then run:
```bash
python -m spacy download en_core_web_sm
```

---

## 🔭 Planned Features

- [ ] Resume-to-multiple-job matching
- [ ] Real-time job scraping (LinkedIn/API)
- [ ] Admin dashboard with auth
- [ ] Power BI Embedded support
- [ ] PDF export of skill analysis
- [ ] Azure Blob for file storage
- [ ] Mobile frontend via .NET MAUI

---

## 📄 License

MIT License — feel free to fork, clone, and extend this project for your own career or portfolio.

---

## 🤝 Contributing

Pull requests and suggestions are welcome! This project is currently under active development as part of a personal learning journey in .NET, data analytics, and full-stack development.
