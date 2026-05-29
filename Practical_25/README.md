# Practical_25

## Project Overview

This solution implements an ASP.NET Core Web API with clean architecture separation across API, Application, Domain, and Infrastructure layers. It uses MediatR for request handling, AutoMapper for mapping, FluentValidation for validation, and Entity Framework Core with SQL Server for persistence.

## Architecture Layers

- `Practical_25.API`: Web API entry point and request orchestration.
- `Practical_25.Application`: Application business logic, commands, queries, handlers, DTOs, mappings, and validation.
- `Practical_25.Domain`: Domain entities, enums, and repository/unit-of-work contracts.
- `Practical_25.Infrastructure`: Database context, repository implementations, migrations, and persistence.

## Folder Structure Tree

```
Practical_25/
├─ Practical_25.slnx
├─ Practical_25.API/
│  ├─ Practical_25.API.csproj
│  ├─ Practical_25.API.csproj.user
│  ├─ Practical_25.API.http
│  ├─ appsettings.json
│  ├─ appsettings.Development.json
│  ├─ Program.cs
│  ├─ Controllers/
│  │  └─ EmployeesController.cs
│  ├─ Properties/
│  │  └─ launchSettings.json
│  ├─ bin/  (compiled output)
│  └─ obj/  (build artifacts)
├─ Practical_25.Application/
│  ├─ Practical_25.Application.csproj
│  ├─ Command/
│  │  ├─ CreateEmployeeCommand.cs
│  │  ├─ DeleteEmployeeCommand.cs
│  │  └─ UpdateEmployeeCommand.cs
│  ├─ DTOs/
│  │  └─ EmployeeResponseDto.cs
│  ├─ Handlers/
│  │  ├─ CreateEmployeeHandler.cs
│  │  ├─ DeleteEmployeeHandler.cs
│  │  ├─ GetAllEmployeesHandler.cs
│  │  ├─ GetByEmployeeIdHandler.cs
│  │  └─ UpdateEmployeeHandler.cs
│  ├─ Interfaces/
│  │  ├─ IEmployeeCommandService.cs
│  │  └─ IEmployeeQueryService.cs
│  ├─ Mappings/
│  │  └─ MappingProfile.cs
│  ├─ Query/
│  │  ├─ GetAllEmployeesQuery.cs
│  │  └─ GetByIdEmployeeQuery.cs
│  ├─ Validators/
│  │  ├─ CreateEmployeeCommandValidator.cs
│  │  ├─ UpdateEmployeeCommandValidator.cs
│  │  └─ ValidationBehaviour.cs
│  ├─ bin/  (compiled output)
│  └─ obj/  (build artifacts)
├─ Practical_25.Domain/
│  ├─ Practical_25.Domain.csproj
│  ├─ Entities/
│  │  ├─ BaseEntity.cs
│  │  └─ Employee.cs
│  ├─ Enums/
│  │  └─ Department.cs
│  ├─ Interfaces/
│  │  ├─ IGenericCommandRepository.cs
│  │  ├─ IGenericQueryRepository.cs
│  │  └─ IUnitOfWork.cs
│  ├─ bin/  (compiled output)
│  └─ obj/  (build artifacts)
└─ Practical_25.Infrastructure/
   ├─ Practical_25.Infrastructure.csproj
   ├─ Data/
   │  └─ AppDbContext.cs
   ├─ Migrations/
   │  ├─ 20260529093157_InitialMigration.cs
   │  ├─ 20260529093157_InitialMigration.Designer.cs
   │  └─ AppDbContextModelSnapshot.cs
   ├─ Repositories/
   │  ├─ GenericCommandRepository.cs
   │  ├─ GenericQueryRepository.cs
   │  └─ UnitOfWork.cs
   ├─ bin/  (compiled output)
   └─ obj/  (build artifacts)
```

## Folder Purpose Summary

### `Practical_25.API`
- Hosts the ASP.NET Core Web API and configures startup services.
- `Program.cs`: configures controllers, DbContext, DI, MediatR, AutoMapper, validators, and Swagger.
- `EmployeesController.cs`: defines the API endpoints and sends MediatR requests.
- `appsettings*.json`: stores configuration for connection strings and environment settings.

### `Practical_25.Application`
- Contains application use cases, validation, mapping, and request/response contracts.
- `Command/`: write-side request models for create, update, delete operations.
- `Query/`: read-side request models for list and detail retrieval.
- `Handlers/`: business logic handlers for MediatR requests.
- `DTOs/`: API response objects decoupling domain entities from the API layer.
- `Mappings/MappingProfile.cs`: AutoMapper profile mapping commands and entities to DTOs.
- `Validators/`: FluentValidation rules and MediatR pipeline behavior.
- `Interfaces/`: service contracts for command/query operations.

### `Practical_25.Domain`
- Defines core business entities, enums, and persistence contracts.
- `Entities/`: domain models representing tables and business state.
- `Enums/Department.cs`: enumerates department values.
- `Interfaces/`: generic repository and unit-of-work abstractions.

### `Practical_25.Infrastructure`
- Implements persistence using Entity Framework Core.
- `Data/AppDbContext.cs`: defines DbSet and model configuration.
- `Repositories/`: generic command/query repositories and UoW implementation.
- `Migrations/`: database schema migration files.

## Important Files and Responsibilities

- `Program.cs`: configures the application startup pipeline and dependency injection.
- `EmployeesController.cs`: exposes CRUD endpoints using MediatR.
- `AppDbContext.cs`: configures `Employees` DbSet and soft-delete query filter.
- `MappingProfile.cs`: maps between commands, `Employee`, and `EmployeeResponseDto`.
- `ValidationBehaviour.cs`: MediatR pipeline middleware that runs fluent validation.
- `GenericCommandRepository.cs`: adds, updates, and soft-deletes entities.
- `GenericQueryRepository.cs`: fetches entities by ID or all records.
- `UnitOfWork.cs`: exposes command/query repositories and commits database changes.
- `CreateEmployeeCommandValidator.cs`: validates create requests.
- `UpdateEmployeeCommandValidator.cs`: validates update requests.
- `EmployeeResponseDto.cs`: response DTO returned by queries.
- `CreateEmployeeCommand.cs`, `UpdateEmployeeCommand.cs`, `DeleteEmployeeCommand.cs`: command models.
- `GetAllEmployeesQuery.cs`, `GetByIdEmployeeQuery.cs`: query models.
- `CreateEmployeeHandler.cs`, `UpdateEmployeeHandler.cs`, `DeleteEmployeeHandler.cs`, `GetAllEmployeesHandler.cs`, `GetByIdHandler.cs`: MediatR handlers.

## SOLID Principles Followed

- Single Responsibility Principle
  - Each file has a focused responsibility: controller, command, handler, validator, repository, or entity.
- Open/Closed Principle
  - Handlers and validators can be extended without modifying existing dispatch logic.
- Liskov Substitution Principle
  - Repository interfaces can be swapped with other implementations because concrete classes conform to contract methods.
- Interface Segregation Principle
  - Separate query and command service contracts keep read/write concerns distinct.
- Dependency Inversion Principle
  - High-level code depends on abstractions (`IUnitOfWork`, generic repositories) instead of concrete implementations.

## Request Flow / Execution Flow

1. Client sends an HTTP request to `EmployeesController`.
2. Controller creates a MediatR request (`Command` or `Query`).
3. MediatR dispatches the request to the appropriate handler.
4. `ValidationBehaviour` runs validators before handler execution.
5. Handler uses `IUnitOfWork` and repositories to perform persistence operations.
6. `UnitOfWork.SaveChangesAsync()` commits the transaction to SQL Server.
7. Handler returns a result or DTO.
8. Controller sends HTTP response back to the client.

## Dependency Injection Usage

- Registered in `Program.cs`:
  - `DbContext`: `AddDbContext<AppDbContext>` with SQL Server.
  - Generic repositories: `IGenericCommandRepository<>`, `IGenericQueryRepository<>`.
  - `IUnitOfWork` mapped to `UnitOfWork`.
  - AutoMapper profile: `MappingProfile`.
  - MediatR handlers: registered from the application assembly.
  - FluentValidation validators: registered from the application assembly.
  - MediatR pipeline behavior: `ValidationBehaviour<,>`.

## Database Usage

- Uses Entity Framework Core with SQL Server.
- `AppDbContext` exposes `DbSet<Employee> Employees`.
- Global query filter on `Employee.Status` implements soft-delete behavior.
- `Employee.Salary` precision configured to `decimal(18,2)`.
- Migration `InitialMigration` creates the `Employees` table with columns for `Id`, `Name`, `Salary`, `DepartmentId`, `EmailId`, `JoiningDate`, and `Status`.

## API Endpoints Summary

| HTTP Method | Route | Request Model | Response | Description |
|-------------|-------|---------------|----------|-------------|
| POST | `/api/employees` | `CreateEmployeeCommand` | `string` | Create a new employee record |
| PUT | `/api/employees` | `UpdateEmployeeCommand` | `string` | Update an existing employee |
| DELETE | `/api/employees/{id}` | route `id` | `string` or `404` | Soft-delete an employee by id |
| GET | `/api/employees` | none | `IEnumerable<EmployeeResponseDto>` | Get all active employees |
| GET | `/api/employees?id={id}` | query `id` | `EmployeeResponseDto` | Get an employee by id |

## Mapping

- `CreateEmployeeCommand -> Employee`
- `UpdateEmployeeCommand -> Employee`
  - Ignores `Id`, `JoiningDate`, and `Status` to preserve the existing entity identity and state.
- `Employee -> EmployeeResponseDto`
  - Maps `DepartmentId` enum to a string representation in the response DTO.

## Detected Architectural Components

- DTOs
  - `EmployeeResponseDto`
- Commands
  - `CreateEmployeeCommand`
  - `UpdateEmployeeCommand`
  - `DeleteEmployeeCommand`
- Queries
  - `GetAllEmployeesQuery`
  - `GetByEmployeeIdQuery`
- Handlers
  - `CreateEmployeeHandler`
  - `UpdateEmployeeHandler`
  - `DeleteEmployeeHandler`
  - `GetAllEmployeesHandler`
  - `GetByIdHandler`
- Services
  - `IEmployeeCommandService` (command contract)
  - `IEmployeeQueryService` (query contract)
- Repositories
  - `IGenericCommandRepository<T>`
  - `IGenericQueryRepository<T>`
  - `GenericCommandRepository<T>`
  - `GenericQueryRepository<T>`
- Entities
  - `Employee`
  - `BaseEntity`
- Validators
  - `CreateEmployeeCommandValidator`
  - `UpdateEmployeeCommandValidator`
  - `ValidationBehaviour<TRequest,TResponse>`
- Middleware
  - MediatR pipeline behavior via `ValidationBehaviour`
- Controllers
  - `EmployeesController`
- Configuration
  - `Program.cs` service registration
  - `appsettings.json` / `appsettings.Development.json`

## Notes

- The application layer defines service interfaces but the current request flow uses MediatR directly from the controller.
- Soft delete is implemented by setting `Employee.Status = false` in `GenericCommandRepository.Delete`.
- The `Department` enum provides strongly typed department values while the response DTO exposes department names as strings.
