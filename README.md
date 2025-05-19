# BookingSystem

**BookingSystem** is a web application built using **ASP.NET Core**, designed to manage and streamline booking processes. The application follows a clean architecture approach with distinct layers for business logic, data access, and domain models. It uses **SQL Server** as the underlying database.

---

## 📌 Technologies Used

- **ASP.NET Core** – Web API framework
- **Entity Framework Core** – ORM for database interactions
- **SQL Server** – Relational database
- **AutoMapper** – Object-to-object mapping
- **xUnit / NUnit / MSTest** – For unit testing (based on your implementation)
- **Dependency Injection** – Built-in support in .NET Core for service management

---

## 🧱 Project Structure

The solution is organized into multiple projects, each with a specific responsibility:

### 1. **BookingSystem (Main Web Project)**
- Hosts the API endpoints
- Handles routing, middleware, and dependency injection configuration

### 2. **BookingSystem.BAL (Business Access Layer)**
- Contains business logic and service interfaces
- Acts as a bridge between controllers and data access logic

### 3. **BookingSystem.DAL (Data Access Layer)**
- Handles interactions with the SQL Server database using EF Core
- Contains repositories and database context

### 4. **BookingSystem.Model (Domain Models)**
- Defines the core domain entities and DTOs (Data Transfer Objects)

### 5. **BookingSystem.Test (Unit Testing)**
- Contains unit tests for business logic and other components
- Ensures robustness and correctness of the application logic

---

## 🚀 Getting Started

### Steps to Run the Project

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Aman-Julka/BookingSystem
   
