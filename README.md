
# 🏢 HR Leave Management System

This project is a **.NET Core Web API** that manages leave types, leave requests, and leave allocations in a typical HR system.  
It uses **Clean Architecture**, **CQRS**, **MediatR**, **Entity Framework Core**, and **FluentValidation**.

---

## 📦 Project Structure

- `Application` → All business logic (handlers, DTOs, validators)
- `Domain` → Core entities (`LeaveType`, `LeaveRequest`, `LeaveAllocation`)
- `Persistence` → Database context + Entity configurations
- `Infrastructure` → External services like Email (optional)
- `API` → The main entry point (Controllers, Program.cs)

---

## 🚀 How to Run the Project

### 1. Prerequisites
- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- SQL Server or SQL Express
- Visual Studio / VS Code

### 2. Update the Connection String

In `appsettings.json`, make sure this line matches your SQL setup:

```json
"ConnectionStrings": {
  "DefaultConnection": "YOUR CONNECTION STRING"
}
```

💡 Tip: Use `Server=(localdb)\MSSQLLocalDB` if you're using LocalDB.

---

### 3. Apply Migrations

```bash
# Run in terminal from root folder
dotnet ef database update
```

---

### 4. Run the App

```bash
dotnet run
```

Navigate to:

```
https://localhost:7140/swagger
```

You'll see the full Swagger UI to test your APIs 🚀

---

## ✅ Features

- ✅ CRUD for Leave Types
- ✅ Submit & Manage Leave Requests
- ✅ Allocate Leave Days to Employees
- ✅ Validation using FluentValidation
- ✅ CQRS pattern using MediatR
- ✅ Swagger UI for testing endpoints

---

## 🧠 Important Concepts

| Concept | Explanation |
|--------|-------------|
| CQRS | Separates Reads (Queries) and Writes (Commands) |
| MediatR | Handles requests between controller and logic |
| DTOs | Data Transfer Objects used to control what data moves |
| Validators | Make sure input is correct before saving |

---

## 📁 Sample Endpoints

- `GET /api/LeaveTypes` → List all leave types
- `POST /api/LeaveTypes` → Create a new leave type
- `GET /api/LeaveRequests` → Get all leave requests
- `POST /api/LeaveAllocation` → Allocate leave for a leave type

---

## 🧑‍💻 Author

Made by **Badri** as part of his .NET learning journey 🚀  
