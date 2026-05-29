# Practical_26 Employee Management API

## Project Overview

A cleanly layered .NET 10 Web API that demonstrates a simple employee management solution built with a Domain/Application/Infrastructure architecture. It exposes employee CRUD operations while applying validation, mapping, dependency injection, and Entity Framework Core persistence.

## Architecture and Layers

- `Practical_26.API`: Web API layer that defines controllers, request routing, validation setup, DI registration, and OpenAPI documentation.
- `Practical_26.Application`: Application layer containing DTOs, commands, service interfaces, validation rules, and mapping logic.
- `Practical_26.Domain`: Domain layer containing core entities, base classes, enums, and repository contracts.
- `Practical_26.Infrastructure`: Infrastructure layer responsible for EF Core data access, repository implementations, unit of work, and database context.
- `.vs`: Visual Studio workspace metadata and hidden build state.

## Folder Structure Tree

```
Practical_26.slnx
.vs/
Practical_26.API/
  appsettings.Development.json
  appsettings.json
  Practical_26.API.csproj
  Practical_26.API.csproj.user
  Practical_26.API.http
  Program.cs
  Controllers/
    EmployeesController.cs
  Properties/
    launchSettings.json
  bin/
  obj/
Practical_26.Application/
  Practical_26.Application.csproj
  Command/
    CreateEmployeeCommand.cs
    UpdateEmployeeCommand.cs
  DTOs/
    EmployeeResponseDto.cs
  Interfaces/
    IEmployeeCommandService.cs
    IEmployeeQueryService.cs
  Mappings/
    MappingProfile.cs
  Services/
    EmployeeCommandService.cs
    EmployeeQueryService.cs
  Validators/
    CreateEmployeeCommandValidator.cs
    UpdateEmployeeCommandValidator.cs
  bin/
  obj/
Practical_26.Domain/
  Practical_26.Domain.csproj
  Entities/
    BaseEntity.cs
    Employee.cs
  Enums/
    Department.cs
  Interfaces/
    IGenericCommandRepository.cs
    IGenericQueryRepository.cs
    IUnitOfWork.cs
  bin/
  obj/
Practical_26.Infrastructure/
  Practical_26.Infrastructure.csproj
  Data/
    AppDbContext.cs
  Migrations/
    20260528101247_InitialMigration.cs
    20260528101247_InitialMigration.Designer.cs
    AppDbContextModelSnapshot.cs
  Repositories/
    GenericCommandRepository.cs
    GenericQueryRepository.cs
    UnitOfWork.cs
  bin/
  obj/
```

> Note: `bin/` and `obj/` folders are generated build artifacts for each project.

## Folder Responsibilities

### `Practical_26.API`
- Hosts the API entry point and DI setup.
- Registers controllers, FluentValidation, AutoMapper, Swagger, and EF Core.
- Contains `EmployeesController` to expose employee endpoints.

### `Practical_26.Application`
- Defines application commands, DTOs, and service interfaces.
- Contains validation rules and mapping profiles.
- Implements business workflows in command/query services.

### `Practical_26.Domain`
- Declares core domain entities and shared base classes.
- Defines repository and unit-of-work contracts.
- Includes `Department` enum values.

### `Practical_26.Infrastructure`
- Implements data access with EF Core and SQL Server.
- Provides generic command/query repositories and a unit-of-work wrapper.
- Configures global query filtering and precision rules in `AppDbContext`.
- Consider Infrastructure layer as **DAL (Data Access Layer)**.

## Important Files and Classes

| File / Class | Responsibility |
|---|---|
| `Program.cs` | Configures services, dependency injection, EF Core, Swagger, and middleware pipeline. |
| `EmployeesController.cs` | Handles HTTP requests for employee CRUD and delegates to application services. |
| `CreateEmployeeCommand.cs` | Command model for creating an employee. |
| `UpdateEmployeeCommand.cs` | Command model for updating an employee. |
| `EmployeeResponseDto.cs` | Response DTO returned to API clients. |
| `MappingProfile.cs` | AutoMapper profile mapping commands/entities/DTOs. |
| `CreateEmployeeCommandValidator.cs` | FluentValidation validator for create requests. |
| `UpdateEmployeeCommandValidator.cs` | FluentValidation validator for update requests. |
| `IEmployeeCommandService.cs` | Command service interface for employee writes. |
| `IEmployeeQueryService.cs` | Query service interface for employee reads. |
| `EmployeeCommandService.cs` | Implements create/update/delete workflows. |
| `EmployeeQueryService.cs` | Implements read workflows for employees. |
| `BaseEntity.cs` | Shared base entity with `Id` and soft-delete `Status`. |
| `Employee.cs` | Domain entity representing an employee record. |
| `Department.cs` | Enum for employee department values. |
| `IGenericCommandRepository.cs` | Generic write repository contract. |
| `IGenericQueryRepository.cs` | Generic read repository contract. |
| `IUnitOfWork.cs` | Unit-of-work contract exposing both query and command repositories. |
| `AppDbContext.cs` | EF Core DbContext with Employee set and query filters. |
| `GenericCommandRepository.cs` | Generic implementation for add/update/delete operations. |
| `GenericQueryRepository.cs` | Generic implementation for get-all/get-by-id operations. |
| `UnitOfWork.cs` | Composes query/command repositories and saves changes. |
| `appsettings.json` | Database connection string and logging configuration. |

## Automatic Feature Detection

- DTOs:
  - `EmployeeResponseDto`
- Commands:
  - `CreateEmployeeCommand`
  - `UpdateEmployeeCommand`
- Query/Query Service:
  - `IEmployeeQueryService`
  - `EmployeeQueryService`
- Command Service / Handlers:
  - `IEmployeeCommandService`
  - `EmployeeCommandService`
- Services:
  - `EmployeeQueryService`
  - `EmployeeCommandService`
- Repositories:
  - `GenericQueryRepository<T>`
  - `GenericCommandRepository<T>`
  - `UnitOfWork`
- Entities:
  - `Employee`
  - `BaseEntity`
- Validators:
  - `CreateEmployeeCommandValidator`
  - `UpdateEmployeeCommandValidator`
- Controllers:
  - `EmployeesController`
- Configurations:
  - `Program.cs`
  - `AppDbContext.cs`
  - `appsettings.json`

## SOLID Principles Followed

- Single Responsibility Principle (SRP): each layer and class has one clear responsibility.
- Open/Closed Principle (OCP): AutoMapper profile and service interfaces allow extension without changing existing implementations.
- Liskov Substitution Principle (LSP): concrete services and repositories implement interfaces consistently.
- Interface Segregation Principle (ISP): separate query and command service interfaces keep contracts focused.
- Dependency Inversion Principle (DIP): `Program.cs` depends on interfaces (`IEmployeeQueryService`, `IEmployeeCommandService`, `IUnitOfWork`) rather than concrete implementations.

## Request / Execution Flow

1. Client sends HTTP request to `EmployeesController`.
2. ASP.NET Core model binding constructs command/query models.
3. FluentValidation validates `CreateEmployeeCommand` and `UpdateEmployeeCommand` automatically.
4. Controller calls application service via injected interfaces.
5. Service uses `IUnitOfWork` and repositories to load or persist domain entities.
6. AutoMapper maps commands to `Employee` entity and entity to `EmployeeResponseDto`.
7. `UnitOfWork.SaveChangesAsync()` commits changes through EF Core.
8. Controller returns HTTP response with mapped DTOs.

## Dependency Injection Usage

- Registered in `Program.cs`:
  - `AddDbContext<AppDbContext>` for EF Core.
  - `AddScoped<IEmployeeCommandService, EmployeeCommandService>`
  - `AddScoped<IEmployeeQueryService, EmployeeQueryService>`
  - `AddScoped<IUnitOfWork, UnitOfWork>`
  - `AddScoped<IGenericCommandRepository<Employee>, GenericCommandRepository<Employee>>`
  - `AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>())`
  - FluentValidation auto-registration via `AddValidatorsFromAssemblyContaining<...>()`

## Database Usage

- Uses SQL Server via EF Core.
- Connection string key: `ConnectionStrings:DefaultConnection` in `appsettings.json`.
- `AppDbContext` configures `DbSet<Employee>` and applies:
  - Global query filter: `e => e.Status` for soft-delete support.
  - `Salary` precision: `18, 2`.
- The `Employee` entity stores employee details, department enum value, email, salary, joining date, and soft-delete status.

## API Endpoints Summary

| Method | Route | Request Body | Description |
|---|---|---|---|
| `POST` | `/api/Employees` | `CreateEmployeeCommand` | Create a new employee. |
| `PUT` | `/api/Employees` | `UpdateEmployeeCommand` | Update an existing employee. |
| `DELETE` | `/api/Employees/{id}` | n/a | Soft-delete (deactivate) an employee by `id`. |
| `GET` | `/api/Employees` | Query string `id` optional | Get all active employees or a single employee by ID. |

### `CreateEmployeeCommand`
- `Name`: string
- `Salary`: decimal
- `DepartmentId`: int
- `EmailId`: string

### `UpdateEmployeeCommand`
- `Id`: Guid
- `Name`: string
- `Salary`: decimal
- `DepartmentId`: int
- `EmailId`: string

### `EmployeeResponseDto`
- `Id`, `Name`, `Salary`, `Department`, `EmailId`, `JoiningDate`, `Status`

## Mapping

- AutoMapper profile is defined in `MappingProfile.cs`.
- Mappings:
  - `CreateEmployeeCommand -> Employee`
  - `UpdateEmployeeCommand -> Employee` (ignores `Id`, `JoiningDate`, and `Status`)
  - `Employee -> EmployeeResponseDto` (maps `DepartmentId` to `Department` string)

## How to Run

1. Open the solution `Practical_26.slnx` in Visual Studio.
2. Ensure SQL Server LocalDB is available or update `appsettings.json` connection string.
3. Build and run the `Practical_26.API` project.
4. Use Swagger UI or an HTTP client against the `/api/Employees` routes.

---

This README summarizes the complete layered architecture, major files, DI setup, request flow, database behavior, and API contract for the Practical_26 solution.