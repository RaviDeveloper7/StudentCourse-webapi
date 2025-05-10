
# ASP.NET Core Web API Demo

This is a beginner-friendly ASP.NET Core Web API project built step-by-step to learn core concepts such as:

- Dependency Injection (DI)
- Middleware
- RESTful API development
- Unit testing (planned)
- Azure readiness (planned)
- Git integration

## 🚀 Technologies Used

- ASP.NET Core 8
- C#
- RESTful Web API
- Dependency Injection
- Git & GitHub
- Swagger / OpenAPI
- Visual Studio 2022 / VS Code

## 📁 Project Structure

```
MyApi/
├── Controllers/
│   └── HomeController.cs
├── Services/
│   ├── IMessageService.cs
│   └── MessageService.cs
├── Program.cs
├── appsettings.json
└── README.md
```

## 🛠️ Features

- ✅ Basic Web API endpoint setup
- ✅ Custom service using Dependency Injection
- ✅ Swagger UI for API testing
- 🚧 Unit Testing (coming soon)
- 🚧 Entity Framework Core integration
- 🚧 Azure services setup

## 🧪 Running the Project

1. Clone the repository  
   `git clone https://github.com/your-username/aspnetcore-webapi-demo.git`

2. Open in Visual Studio or VS Code

3. Run the project  
   `dotnet run` or use `F5` in Visual Studio

4. Open Swagger UI  
   Navigate to: `https://localhost:xxxx/swagger`

## 🔁 API Example

```
GET /api/home
Response: "Hello from MessageService!"
```

## 📌 Learning Goals

This project is designed to help me understand and explain:

- How dependency injection works in ASP.NET Core
- What middleware is and how it's used
- How controllers and services communicate
- How to structure an API for scalability
- How to prepare the codebase for cloud readiness

## 📅 Roadmap

- [x] Git setup
- [x] Basic controller and DI service
- [ ] Add EF Core & DBContext
- [ ] Unit testing using xUnit
- [ ] Azure service integration
- [ ] Logging and monitoring (App Insights)

---

Feel free to fork, learn, or suggest improvements!
