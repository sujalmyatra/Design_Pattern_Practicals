# Practical_24

## Project Overview

`Practical_24` is a layered .NET 10 solution that exposes an ASP.NET Core Web API for employee management. The solution follows a clean architecture style with separate API, Application, Domain, and Infrastructure layers.

> Note: The `Practical_24.Infrastructure` layer is treated as the DAL (data access layer) in this solution.

## Architecture Summary

- `Practical_24.API` - API layer with controllers, configuration, and request pipeline setup.
- `Practical_24.Application` - Application layer with DTOs, service interfaces, mappings, validators, and business logic.
- `Practical_24.Domain` - Domain layer containing entities, enums, and repository/unit-of-work interfaces.
- `Practical_24.Infrastructure` - Infrastructure/DAL layer with EF Core context, repositories, unit of work, and migrations.

## Folder Structure Tree

```
Practical_24
├── Practical_24.slnx
├── Practical_24.API
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Practical_24.API.csproj
│   ├── Practical_24.API.csproj.user
│   ├── Practical_24.API.http
│   ├── Program.cs
│   ├── Controllers
│   │   └── EmployeesController.cs
│   ├── Properties
│   │   └── launchSettings.json
│   ├── bin/
│   └── obj/
├── Practical_24.Application
│   ├── Practical_24.Application.csproj
│   ├── DTOs
│   │   ├── CreateEmployeeDto.cs
│   │   ├── EmployeeResponseDto.cs
│   │   └── UpdateEmployeeDto.cs
│   ├── Interfaces
│   │   └── IEmployeeService.cs
│   ├── Mappings
│   │   └── MappingProfile.cs
│   ├── Services
│   │   └── EmployeeService.cs
│   ├── Validators
│   │   ├── CreateEmployeeDtoValidator.cs
│   │   └── UpdateEmployeeDtoValidator.cs
│   ├── bin/
│   └── obj/
├── Practical_24.Domain
│   ├── Practical_24.Domain.csproj
│   ├── Entities
│   │   ├── BaseEntity.cs
│   │   └── Employee.cs
│   ├── Enums
│   │   └── Department.cs
│   ├── Interfaces
│   │   ├── IGenericRepository.cs
│   │   └── IUnitOfWork.cs
│   ├── bin/
│   └── obj/
├── Practical_24.Infrastructure
│   ├── Practical_24.Infrastructure.csproj
│   ├── Data
│   │   └── AppDbContext.cs
│   ├── Migrations
│   │   ├── 20260527132817_InitialMigration.cs
│   │   ├── 20260527132817_InitialMigration.Designer.cs
│   │   └── AppDbContextModelSnapshot.cs
│   ├── Repositories
│   │   ├── EmployeeRepository.cs
│   │   ├── GenericRepository.cs
│   │   └── UnitOfWork.cs
│   ├── bin/
│   └── obj/
└── .vs/
```

## Layer and File Responsibilities

### Root
- `Practical_24.slnx` - solution file that groups all projects.

### Practical_24.API
- `Program.cs` - configures services, middleware, DI, database, Swagger, and maps controllers.
- `Controllers/EmployeesController.cs` - defines employee CRUD endpoints and delegates work to the application service.
- `appsettings.json` - stores logging and SQL Server connection string.
- `appsettings.Development.json` - local development configuration overrides.
- `launchSettings.json` - local IIS Express and application launch settings.

### Practical_24.Application
- `DTOs/CreateEmployeeDto.cs` - request payload for creating an employee.
- `DTOs/UpdateEmployeeDto.cs` - request payload for updating an employee.
- `DTOs/EmployeeResponseDto.cs` - API response shape for employee data.
- `Interfaces/IEmployeeService.cs` - defines the employee service contract.
- `Services/EmployeeService.cs` - implements use-case logic for creating, updating, deleting, and retrieving employees.
- `Mappings/MappingProfile.cs` - configures AutoMapper mappings between DTOs and domain entities.
- `Validators/CreateEmployeeDtoValidator.cs` - validates create payload rules.
- `Validators/UpdateEmployeeDtoValidator.cs` - validates update payload rules.

### Practical_24.Domain
- `Entities/BaseEntity.cs` - base domain entity with `Id` and `status` fields.
- `Entities/Employee.cs` - employee entity model representing the data structure persisted in the database.
- `Enums/Department.cs` - employee department enumeration values.
- `Interfaces/IGenericRepository.cs` - generic repository contract for common CRUD operations.
- `Interfaces/IUnitOfWork.cs` - unit-of-work contract exposing employee repository and commit behavior.

### Practical_24.Infrastructure (DAL)
- `Data/AppDbContext.cs` - EF Core database context with `Employees` DbSet and query filter for active records.
- `Repositories/GenericRepository.cs` - generic EF Core repository implementation.
- `Repositories/EmployeeRepository.cs` - concrete repository for `Employee` entities.
- `Repositories/UnitOfWork.cs` - unit-of-work implementation exposing `Employees` repository and save changes.
- `Migrations/` - EF Core migration artifacts for database schema creation.

## Key Components

- DTOs: `CreateEmployeeDto`, `UpdateEmployeeDto`, `EmployeeResponseDto`
- Services: `EmployeeService`
- Repositories: `GenericRepository<T>`, `EmployeeRepository`
- Entities: `Employee`, `BaseEntity`
- Validators: `CreateEmployeeDtoValidator`, `UpdateEmployeeDtoValidator`
- Controllers: `EmployeesController`
- Configurations: `Program.cs`, `appsettings.json`, `AppDbContext.cs`
- Mapping: `MappingProfile.cs`

## Dependency Injection Usage

The application uses DI in `Program.cs`:

- `IGenericRepository<>` → `GenericRepository<>`
- `IUnitOfWork` → `UnitOfWork`
- `IEmployeeService` → `EmployeeService`
- `AppDbContext` configured with SQL Server
- AutoMapper and FluentValidation registered from assembly types

This decouples controller and service layers from concrete implementation details.

## Request Flow / Execution Flow

1. Client calls an endpoint on `EmployeesController`.
2. Controller receives DTO payload and calls `IEmployeeService`.
3. `EmployeeService` maps DTOs to domain entities using AutoMapper.
4. Service uses `IUnitOfWork.Employees` to add, update, delete, or read data.
5. `UnitOfWork` delegates repository operations to `GenericRepository<T>`.
6. `AppDbContext` commits the transaction with `SaveChangesAsync()`.
7. Service returns mapped `EmployeeResponseDto` results to the controller.
8. Controller returns HTTP response.

## Database Usage

- Database provider: SQL Server
- Connection string configured in `Practical_24.API/appsettings.json`
- `AppDbContext` exposes `DbSet<Employee> Employees`
- Global query filter applies `status == true` for active records
- `Employee.Salary` precision configured to `18,2`
- Migrations are stored under `Practical_24.Infrastructure/Migrations`

## API Endpoints Summary

| HTTP Verb | Route | Description |
| --- | --- | --- |
| POST | `/api/employees` | Create a new employee.
| PUT | `/api/employees` | Update an existing employee.
| DELETE | `/api/employees/{id}` | Deactivate an employee by ID.
| GET | `/api/employees` | Get all active employees or query by `id`.

## Mapping

AutoMapper maps the following:

- `CreateEmployeeDto` → `Employee`
- `UpdateEmployeeDto` → `Employee` (ignores `Id`, `JoiningDate`, and `status` during mapping)
- `Employee` → `EmployeeResponseDto` (maps `DepartmentId` enum to a string `Department` field)

## SOLID Principles Followed

- Single Responsibility: each project layer and class has a focused responsibility.
- Open/Closed: repositories and services depend on abstractions, enabling extension through new implementations.
- Liskov Substitution: interfaces like `IGenericRepository<T>` can be substituted with repository implementations.
- Interface Segregation: service and repository interfaces expose focused contracts for their consumers.
- Dependency Inversion: high-level modules depend on abstractions (`IEmployeeService`, `IUnitOfWork`, `IGenericRepository<>`) instead of concrete classes.

## Notes

- There are no explicit CQRS command/query/handler classes; the service layer acts as the application-level handler.
- No custom middleware is defined beyond the standard ASP.NET Core pipeline.
- Build artifact folders such as `bin/`, `obj/`, and `.vs/` are generated output and are included in the tree for completeness.
