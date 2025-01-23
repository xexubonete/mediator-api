# 🚀 Mediator API with Docker

A .NET Core Web API project implementing the Mediator pattern with CQRS, Entity Framework Core, and Docker support.

## 📋 Overview

This project is a RESTful API for managing products, built with:
- .NET 8.0
- Entity Framework Core
- MediatR (CQRS pattern)
- SQL Server
- Docker
- Swagger/OpenAPI

## ⭐ Features

- Complete CRUD operations for Products
- API Key Authentication
- Swagger Documentation
- Docker containerization
- SQL Server database
- Entity Framework Core with Code First approach
- CQRS implementation using MediatR

## 📁 Project Structure

```
├── Dockerfile
├── LICENSE
├── README.md
├── docker-compose.yml
├── mediator-api
│   ├── Commands
│   │   ├── CreateProductCommand.cs
│   │   ├── DeleteProductCommand.cs
│   │   └── UpdateProductCommand.cs
│   ├── Controllers
│   │   ├── ApiController.cs
│   │   └── ProductController.cs
│   ├── DependencyInjection.cs
│   ├── Entities
│   │   └── Product.cs
│   ├── Interfaces
│   │   └── IApiDbContext.cs
│   ├── Middlewares
│   │   └── AuthMiddleware.cs
│   ├── Persistence
│   │   ├── ApiDbContext.cs
│   │   └── Migrations
│   │       ├── 20240817183205_Initial_Migration.Designer.cs
│   │       ├── 20240817183205_Initial_Migration.cs
│   │       └── ApiDbContextModelSnapshot.cs
│   ├── Program.cs
│   ├── Properties
│   │   └── launchSettings.json
│   ├── Queries
│   │   ├── GetAllProductsQuery.cs
│   │   ├── GetProductByIdQuery.cs
│   │   └── GetProductByNameQuery.cs
│   ├── appsettings.Development.json
│   └── appsettings.json
├── mediator-api.Test
│   ├── Commands
│   │   └── CreateProductCommandTests.cs
│   └── mediator-api.Test.csproj
└── mediator-api.sln
```

## 🏁 Getting Started

### Prerequisites

- Docker Desktop
- .NET 8.0 SDK
- SQL Server

### Running with Docker

1. Clone the repository
2. Navigate to the root directory
3. Run Docker Compose:
```bash
docker-compose up -d
```

The API will be available at `http://localhost:5024/swagger`

### 🔐 API Authentication

All endpoints are protected with API Key authentication. Include the following header in your requests:
```
api_key: <your_api_key>
```

## 🛣️ API Endpoints

### Products

- `GET /api/Product` - Get all products
- `GET /api/Product/id` - Get product by ID
- `GET /api/Product/name` - Get product by name
- `POST /api/Product/create` - Create new product(s)
- `PUT /api/Product/{id}` - Update a product
- `DELETE /api/Product/{id}` - Delete a product

## 💾 Database

The project uses SQL Server with Entity Framework Core. The database will be automatically created on first run with the initial migration.

Connection string (in Docker environment):
```
Server=YourServer;Database=CrudApi;User Id=YourUsername;Password=YourPassword;TrustServerCertificate=True
```

## 👨‍💻 Development

### Adding New Migrations

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Running Tests

```bash
dotnet test
```

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

The API will be available at `http://localhost:5024/swagger`
