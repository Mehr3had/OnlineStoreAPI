# OnlineStoreAPI

A RESTful E-commerce Web API built with ASP.NET Core 8 and Entity Framework Core.

## About The Project

OnlineStoreAPI is a backend project for an e-commerce application.

The project was developed to practice building a structured and maintainable Web API using a layered architecture and common backend development patterns.

The API provides functionality for managing products, categories, users, shopping carts, orders, payments, reviews, and product images.

## Features

* Product and category management
* Shopping cart management
* Order checkout
* Payment processing
* Product reviews
* Product image management
* User registration and login
* JWT authentication
* Role-based authorization
* User ownership authorization
* Password hashing
* DTO-based API responses
* Partial updates using PATCH
* Global exception handling
* Request validation with FluentValidation
* RESTful API design

## Technologies

* C# / .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* SQL Server
* JWT
* FluentValidation
* Swagger / OpenAPI
* Git & GitHub

## Architecture

The project follows a layered architecture using:

* **Controllers** — Handle HTTP requests and responses.
* **Services** — Contain business logic.
* **Repositories** — Handle database operations.
* **Unit of Work** — Coordinates repositories and database transactions.
* **DTOs** — Control the data exposed through the API.
* **Middleware** — Handles exceptions globally.

### Request Flow

```text
Client
   ↓
Controller
   ↓
Service
   ↓
Unit of Work
   ↓
Repository
   ↓
Entity Framework Core
   ↓
SQL Server
```

## Main Entities

The main entities in the system include:

* User
* Role
* Category
* Product
* ProductImage
* Cart
* CartItem
* Address
* Order
* OrderItem
* Payment
* Review

The database contains relationships such as:

* Category → Products
* User → Orders
* User → Addresses
* User → Cart
* Cart → CartItems
* Product → Reviews
* Product → ProductImages
* Order → OrderItems
* Order → Payment

## Authentication & Authorization

The API uses JWT Bearer Authentication.

After a successful login, the server generates a JWT containing user information such as:

* User ID
* Email
* Role

The client sends the token with protected requests using the `Authorization` header.

Example:

```http
Authorization: Bearer <token>
```

The API also implements role-based authorization.

For example, product deletion is restricted to users with the `Admin` role.

The project also checks resource ownership for user-specific operations such as:

* Cart access
* Orders
* Payments

This prevents authenticated users from accessing another user's resources.

## Validation & Error Handling

The project uses **FluentValidation** for validating incoming DTOs.

A global exception middleware is also implemented to convert application exceptions into appropriate HTTP responses.

Examples:

```text
400 Bad Request
404 Not Found
409 Conflict
500 Internal Server Error
```

## RESTful API

The API follows RESTful principles by using resources and standard HTTP methods.

Examples:

```http
GET    /api/Product
GET    /api/Product/{id}
POST   /api/Product
PUT    /api/Product/{id}
PATCH  /api/Product/{id}
DELETE /api/Product/{id}
```

User-specific resources use `/me` where appropriate:

```http
GET  /api/Cart/me
POST /api/Cart/me/items

GET  /api/Order/me
POST /api/Order/me/checkout
```

## Example API Workflow

A typical shopping flow can be:

```text
Register / Login
      ↓
Receive JWT
      ↓
Browse Products
      ↓
Add Product to Cart
      ↓
Select Address
      ↓
Checkout
      ↓
Create Order
      ↓
Make Payment
```

## How To Run

### 1. Clone the repository

```bash
git clone https://github.com/Mehr3had/OnlineStoreAPI.git
```

### 2. Navigate to the project

```bash
cd OnlineStoreAPI
```

### 3. Configure the database

Update the connection string according to your local SQL Server configuration.

### 4. Configure JWT Secret

The JWT signing key is stored using ASP.NET Core User Secrets and is not included in the repository.

Set it using:

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_DEVELOPMENT_SECRET"
```

### 5. Apply migrations

```bash
dotnet ef database update
```

### 6. Run the API

```bash
dotnet run
```

Swagger will be available through the application's configured Swagger URL.

## Project Status

The project is currently completed as a backend practice project and is open for further improvements and additional features.

## Author

**Mehrshad Gohari**

Computer Engineering Student
C# / ASP.NET Core Backend Developer
