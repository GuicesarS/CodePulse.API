# CodePulse.API

A **real-world REST API** built with **ASP.NET Core Web API** and integrated with a modern **Angular 20** frontend.
This project was developed with the goal of applying full stack concepts, best practices, and real production-like architecture.

---

## 🔧 Technologies

<p align="center">
  <!-- Languages -->
  <img src="https://skillicons.dev/icons?i=cs,ts,html,css,bootstrap" height="40" />
</p>

<p align="center">
  <!-- Frameworks & Tools -->
  <img src="https://skillicons.dev/icons?i=dotnet,angular,github,postman" height="40" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" width="40" height="40" alt="SQL Server" />
</p>

> Full stack project focused on clean architecture, REST principles, and seamless frontend–backend integration.

---

## 📌 About the Project

**CodePulse** is a blog management system that exposes a RESTful API to handle blog posts, categories, and authentication.
The backend is responsible for business rules, data persistence, and security, while the Angular frontend consumes the API and provides a responsive user experience.

This repository contains the **API layer** of the application.

---

## ✨ Features

* JWT-based authentication and authorization
* CRUD operations for blog posts
* CRUD operations for categories
* Many-to-many relationship between posts and categories
* Entity Framework Core with SQL Server
* Data validation and error handling
* Swagger / OpenAPI documentation
* CORS configuration for Angular integration

---

## 🧱 Architecture Overview

* **Controllers** – Handle HTTP requests and responses
* **Repositories** – Encapsulate data access logic
* **Domain Models** – Represent business entities
* **DTOs** – Data transfer between API and client
* **EF Core** – ORM for database access

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

* .NET SDK (latest LTS)
* SQL Server (LocalDB or local instance)
* SQL Server Management Studio (SSMS)
* Visual Studio 2022
* Node.js (LTS)
* Angular CLI

---

### 🔹 Backend Setup

Clone the repository:

```bash
git clone https://github.com/GuicesarS/CodePulse.API.git
cd CodePulse.API
```

Restore dependencies and apply migrations:

```bash
dotnet restore
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

The API will be available at:

```
https://localhost:7196
```

Swagger documentation:

```
https://localhost:7196/swagger
```

---

## 🔐 Authentication

The API uses **JWT (JSON Web Token)** for authentication.

* Login endpoint issues an access token
* Token is validated on protected endpoints
* Role-based authorization is supported

---

## 📡 Example Endpoints

```http
GET    /api/BlogPost
GET    /api/BlogPost/{id}
POST   /api/BlogPost
PUT    /api/BlogPost/{id}
DELETE /api/BlogPost/{id}
```

```http
GET    /api/Categories
POST   /api/Categories
PUT    /api/Categories/{id}
```

---

## 🧪 Testing & Development

* Endpoints can be tested using **Swagger** or **Postman**
* Entity Framework Core migrations manage database schema
* CORS is configured for local Angular development

---

## 👨‍💻 Author

Developed by **Guilherme César Soares**

---

## 📄 License

This project is licensed under the **MIT License**.
See the `LICENSE` file for more details.
