# Practical_22

## Project Overview

Practical_22 is a layered .NET 10 Web API for employee management. The solution is organized into four core projects: 
- `Practical_22.API` as the presentation layer,
- `Practical_22.Application` as the BAL (Business Access Layer),
- `Practical_22.Domain` as the domain model layer,
- `Practical_22.Infrastructure` as the DAL (Data Access Layer).

This project implements CRUD operations for employees with validation, mapping, logging, and EF Core persistence.

> Note: The Infrastructure layer is treated as the DAL, and the Application layer is treated as the BAL.

---

## Folder Structure

```
Practical_22/
│   Practical_22.slnx
│
├── Practical_22.API/
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   Logs.txt
│   │   Practical_22.API.csproj
│   │   Practical_22.API.csproj.user
│   │   Practical_22.API.http
│   │   Program.cs
│   ├── bin/ (build output)
│   ├── obj/ (build output)
│   └── Controllers/
│       └── EmployeesController.cs
│
├── Practical_22.Application/
│   │   Practical_22.Application.csproj
│   ├── DTOs/
│   │   ├── CreateEmployeeDto.cs
│   │   ├── EmployeeResponseDto.cs
│   │   └── UpdateEmployeeDto.cs
│   ├── Interfaces/
│   │   └── IEmployeeService.cs
│   ├── Mappings/
│   │   └── MappingProfile.cs
│   ├── Services/
│   │   └── EmployeeService.cs
│   ├── Validators/
│   │   ├── CreateEmployeeDtoValidator.cs
│   │   └── UpdateEmployeeDtoValidator.cs
│   ├── bin/ (build output)
│   └── obj/ (build output)
│
├── Practical_22.Domain/
│   │   Practical_22.Domain.csproj
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   └── Employee.cs
│   ├── Enums/
│   │   └── Department.cs
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs
│   │   ├── ILoggerService.cs
│   │   └── IUnitOfWork.cs
│   ├── bin/ (build output)
│   └── obj/ (build output)
│
└── Practical_22.Infrastructure/
    │   Practical_22.Infrastructure.csproj
    ├── Data/
    │   └── AppDbContext.cs
    ├── Logging/
    │   └── FileLogger.cs
    ├── Migrations/
    │   ├── 20260527124936_InitialMigration.cs
    │   ├── 20260527124936_InitialMigration.Designer.cs
    │   └── AppDbContextModelSnapshot.cs
    ├── Repositories/
    │   ├── GenericRepository.cs
    │   └── UnitOfWork.cs
    ├── bin/ (build output)
    └── obj/ (build output)
```

---

## Key Files and Responsibilities

### Root
- `Practical_22.slnx` - Visual Studio solution file tying all projects together.

### Practical_22.API
- `Program.cs` - configures DI, middleware, EF Core, AutoMapper, FluentValidation, and Swagger.
- `EmployeesController.cs` - HTTP API controller for employee CRUD endpoints.
- `appsettings.json` - database connection string and logging settings.
- `Practical_22.API.http` - HTTP request examples for manual API testing.
- `Logs.txt` - runtime log output produced by the singleton file logger.

### Practical_22.Application (BAL)
- `CreateEmployeeDto.cs` - DTO for create employee requests.
- `UpdateEmployeeDto.cs` - DTO for update employee requests.
- `EmployeeResponseDto.cs` - DTO for responses returned from service methods.
- `IEmployeeService.cs` - service interface defining employee operations.
- `EmployeeService.cs` - business logic implementing create/update/delete/get operations.
- `MappingProfile.cs` - AutoMapper profile for DTO-to-entity and entity-to-DTO mapping.
- `CreateEmployeeDtoValidator.cs` - input validation for create requests.
- `UpdateEmployeeDtoValidator.cs` - input validation for update requests.

### Practical_22.Domain
- `BaseEntity.cs` - common entity base with `Id` and `status` soft-delete flag.
- `Employee.cs` - domain entity representing an employee.
- `Department.cs` - enum for department IDs.
- `IGenericRepository.cs` - generic repository contract for common CRUD methods.
- `IUnitOfWork.cs` - unit-of-work contract exposing employee repository and save.
- `ILoggerService.cs` - logging abstraction used by services.

### Practical_22.Infrastructure (DAL)
- `AppDbContext.cs` - EF Core DbContext with `Employees` DbSet and query filter.
- `FileLogger.cs` - singleton logger that writes messages to `Logs.txt`.
- `GenericRepository.cs` - EF Core generic repository implementation.
- `UnitOfWork.cs` - concrete unit-of-work wiring repository to DbContext.
- `Migrations/` - EF Core migration files defining database schema.

---

## Architecture and Layers

### API Layer
- Exposes REST endpoints via `EmployeesController`.
- Uses controller actions to invoke `IEmployeeService`.
- Configures Swagger, validation, DB context, and mapping.

### Application Layer (BAL)
- Contains business logic, DTOs, validators, and mapping.
- Validates client input with FluentValidation.
- Maps between DTOs and domain entities using AutoMapper.
- Delegates persistence to the DAL via `IUnitOfWork`.

### Domain Layer
- Encapsulates core domain models and shared abstractions.
- Defines entity contracts, repository contracts, and logging abstraction.

### Infrastructure Layer (DAL)
- Implements data access using EF Core.
- Provides repository and unit-of-work patterns.
- Contains logging and database context configuration.

---

## Dependency Injection Usage

Configured in `Practical_22.API/Program.cs`:
- `AddDbContext<AppDbContext>(...)` - registers EF Core DbContext.
- `AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))` - generic repository DI.
- `AddScoped<IUnitOfWork, UnitOfWork>()` - unit-of-work DI.
- `AddScoped<IEmployeeService, EmployeeService>()` - business service DI.
- `AddSingleton<ILoggerService>(FileLogger.Instance)` - singleton logger DI.
- `AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>())` - AutoMapper registration.
- `AddValidatorsFromAssemblyContaining<...>()` - FluentValidation registration.

This means controllers receive service abstractions, and services receive persistence abstractions.

---

## Database Usage

- Uses SQL Server LocalDB with connection string in `appsettings.json`.
- `AppDbContext` defines `DbSet<Employee> Employees`.
- `OnModelCreating` adds:
  - global query filter for `status` soft delete,
  - salary precision configuration.
- Migrations in `Practical_22.Infrastructure/Migrations` define schema and snapshot.
- `UnitOfWork.SaveChangesAsync()` persists changes through EF Core.

---

## API Endpoints Summary

| Method | Route | Description |
|---|---|---|
| POST | `/api/Employees` | Create a new employee from `CreateEmployeeDto`. |
| PUT | `/api/Employees` | Update an existing employee using `UpdateEmployeeDto`. |
| DELETE | `/api/Employees/{id}` | Soft-delete an employee by `id` (sets `status = false`). |
| GET | `/api/Employees` | Retrieve all active employees or filtered by optional `id` query parameter. |

`EmployeesController` returns `Ok(...)` responses for success and `NotFound()` when deleting a missing employee.

---

## Request / Execution Flow

1. Client sends HTTP request to `EmployeesController`.
2. Controller model binds request data to DTOs.
3. FluentValidation validates DTO input.
4. Controller calls `IEmployeeService`.
5. `EmployeeService` logs the operation and maps DTOs to domain entities.
6. Service uses `IUnitOfWork.Employees` repository for persistence.
7. `GenericRepository<Employee>` talks to `AppDbContext`.
8. `UnitOfWork.SaveChangesAsync()` commits EF Core changes to SQL Server.
9. Service returns mapped `EmployeeResponseDto` back to controller.
10. Controller returns HTTP result to the caller.

---

## Mapping

`MappingProfile` defines:
- `CreateEmployeeDto -> Employee`
- `UpdateEmployeeDto -> Employee` with ignored `Id`, `JoiningDate`, and `status`
- `Employee -> EmployeeResponseDto` converting department enum to string.

AutoMapper is registered globally in `Program.cs` and used in `EmployeeService`.

---

## SOLID Principles Followed

- **Single Responsibility**: controllers handle HTTP, services handle business logic, repositories handle data access.
- **Open/Closed**: abstractions like `IEmployeeService`, `IGenericRepository<T>`, `IUnitOfWork`, and `ILoggerService` allow new implementations without modifying existing callers.
- **Liskov Substitution**: concrete services and repositories implement interfaces and can be replaced by alternative implementations.
- **Interface Segregation**: smaller interfaces such as `IEmployeeService`, `ILoggerService`, and `IGenericRepository<T>` keep contracts focused.
- **Dependency Inversion**: high-level layers depend on interfaces (`IEmployeeService`, `IUnitOfWork`, `ILoggerService`) rather than concrete types.

---

## Detected Components

- DTOs: `CreateEmployeeDto`, `UpdateEmployeeDto`, `EmployeeResponseDto`.
- Services: `EmployeeService`, `IEmployeeService`.
- Repositories: `GenericRepository<T>`, `IGenericRepository<T>`, `UnitOfWork`, `IUnitOfWork`.
- Entities: `Employee`, `BaseEntity`.
- Validators: `CreateEmployeeDtoValidator`, `UpdateEmployeeDtoValidator`.
- Middleware / Pipeline: standard ASP.NET pipeline configured in `Program.cs` (`UseSwagger`, `UseSwaggerUI`, `UseHttpsRedirection`, `UseAuthorization`).
- Controllers: `EmployeesController`.
- Configuration: `appsettings.json`, `appsettings.Development.json`, `Program.cs`.
- Commands/Queries/Handlers: not explicitly implemented as distinct CQRS classes; controller actions use service methods directly.

---

## Notes

- The `Infrastructure` project is the DAL.
- The `Application` project is the BAL.
- `Logs.txt` is populated by `FileLogger.Instance` when service methods execute.
- `obj/` and `bin/` directories are build artifacts and contain compiled output and generated files.
- No custom middleware classes are defined beyond the standard ASP.NET Core middleware pipeline.
