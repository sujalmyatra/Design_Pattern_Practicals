**Project Overview**

- **Practical_23**: A small .NET Web API demonstrating layered architecture, dependency injection, EF Core data access, AutoMapper, FluentValidation, and design patterns (Factory / Abstract Factory).

**Architecture & Layers**

- **Presentation (API)**: Handles HTTP requests and exposes controllers and endpoints.
- **Application (BAL)**: Business Access Layer — contains DTOs, services, validators, mapping, and factory implementations.
- **Domain**: Core domain models, entities, enums and domain-level interfaces.
- **Infrastructure (DAL)**: Data Access Layer — EF Core DbContext, repositories, unit-of-work, and migrations.

Note: Consider the Infrastructure layer as DAL (data access layer) and the Application layer as BAL (business access layer).

**Folder Structure**

- [Practical_23.slnx](Practical_23.slnx)
- [Practical_23.API](Practical_23.API)
  - Purpose: Web API project exposing endpoints and wiring DI/configuration.
  - Important files:
    - [Practical_23.API.csproj](Practical_23.API/Practical_23.API.csproj): Project file.
    - [Program.cs](Practical_23.API/Program.cs): App startup, DI registrations, Swagger, EF Core setup.
    - [appsettings.json](Practical_23.API/appsettings.json): Configuration (connection strings).
    - [appsettings.Development.json](Practical_23.API/appsettings.Development.json): Dev overrides.
    - [Practical_23.API.http](Practical_23.API/Practical_23.API.http): simple HTTP sample used for testing.
    - [Controllers/EmployeesController.cs](Practical_23.API/Controllers/EmployeesController.cs): API controller for Employee operations.

- [Practical_23.Application](Practical_23.Application)
  - Purpose: BAL - DTOs, services, factories, validators, AutoMapper profiles and business logic.
  - Important files/classes:
    - [Practical_23.Application.csproj](Practical_23.Application/Practical_23.Application.csproj): Project file.
    - DTOs (data transfer objects):
      - [DTOs/CreateEmployeeDto.cs](Practical_23.Application/DTOs/CreateEmployeeDto.cs): Create payload.
      - [DTOs/UpdateEmployeeDto.cs](Practical_23.Application/DTOs/UpdateEmployeeDto.cs): Update payload.
      - [DTOs/EmployeeResponseDto.cs](Practical_23.Application/DTOs/EmployeeResponseDto.cs): Response DTO.
      - [DTOs/OvertimeRequestDto.cs](Practical_23.Application/DTOs/OvertimeRequestDto.cs): Overtime calculation request.
    - Services:
      - [Services/EmployeeService.cs](Practical_23.Application/Services/EmployeeService.cs): Implements `IEmployeeService` — orchestrates repository calls, mapping and factory usage.
    - Interfaces:
      - [Interfaces/IEmployeeService.cs](Practical_23.Application/Interfaces/IEmployeeService.cs): Service contract used by controllers.
      - [Interfaces/IOverTimePay.cs](Practical_23.Application/Interfaces/IOverTimePay.cs): Overtime calculator contract.
    - Mapping:
      - [Mappings/MappingProfile.cs](Practical_23.Application/Mappings/MappingProfile.cs): AutoMapper profile mapping between DTOs and Entities.
    - Validators:
      - [Validators/CreateEmployeeDtoValidator.cs](Practical_23.Application/Validators/CreateEmployeeDtoValidator.cs): FluentValidation rules for create.
      - [Validators/UpdateEmployeeDtoValidator.cs](Practical_23.Application/Validators/UpdateEmployeeDtoValidator.cs): FluentValidation rules for update.
    - Factories & Patterns:
      - [Abstract Factory/AbstractFactoryProvider.cs](Practical_23.Application/Abstract%20Factory/AbstractFactoryProvider.cs): Selects appropriate abstract factory by department.
      - [Abstract Factory/IndoorFactory.cs](Practical_23.Application/Abstract%20Factory/IndoorFactory.cs) and [OutdoorFactory.cs](Practical_23.Application/Abstract%20Factory/OutdoorFactory.cs): Group department-specific factories.
      - [Factories/*Factory.cs](Practical_23.Application/Factories): Concrete factories returning overtime calculators.
      - [Products/*OverTimePay.cs](Practical_23.Application/Products): Concrete `IOverTimePay` implementations.

- [Practical_23.Domain](Practical_23.Domain)
  - Purpose: Core domain types and contracts.
  - Important files:
    - [Practical_23.Domain.csproj](Practical_23.Domain/Practical_23.Domain.csproj): Project file.
    - Entities:
      - [Entities/BaseEntity.cs](Practical_23.Domain/Entities/BaseEntity.cs): Base entity with `Id` and soft-delete `status`.
      - [Entities/Employee.cs](Practical_23.Domain/Entities/Employee.cs): Employee domain model.
    - Enums:
      - [Enums/Department.cs](Practical_23.Domain/Enums/Department.cs): Department enum used by factories.
    - Interfaces:
      - [Interfaces/IGenericRepository.cs](Practical_23.Domain/Interfaces/IGenericRepository.cs): Generic repository contract.
      - [Interfaces/IUnitOfWork.cs](Practical_23.Domain/Interfaces/IUnitOfWork.cs): Unit of work/exposed repositories.

- [Practical_23.Infrastructure](Practical_23.Infrastructure)
  - Purpose: DAL - EF Core DbContext, repository implementations and migrations.
  - Important files:
    - [Practical_23.Infrastructure.csproj](Practical_23.Infrastructure/Practical_23.Infrastructure.csproj): Project file.
    - [Data/AppDbContext.cs](Practical_23.Infrastructure/Data/AppDbContext.cs): EF Core DbContext and model configuration.
    - [Repositories/GenericRepository.cs](Practical_23.Infrastructure/Repositories/GenericRepository.cs): Generic EF-backed repository implementation.
    - [Repositories/UnitOfWork.cs](Practical_23.Infrastructure/Repositories/UnitOfWork.cs): Concrete UnitOfWork exposing `Employees` repository and SaveChanges.
    - [Migrations/*](Practical_23.Infrastructure/Migrations): EF Core migrations and model snapshot.

- Build / tooling / artifacts
  - [bin/](bin/) and [obj/](obj/): Compiled outputs and intermediate files (ignored in source control normally).
  - [.vs/](.vs/): Visual Studio workspace files.

**Detected Components / Responsibilities**

- DTOs: CreateEmployeeDto, UpdateEmployeeDto, EmployeeResponseDto, OvertimeRequestDto.
- Controllers: EmployeesController (API endpoints).
- Services: EmployeeService implements business operations and orchestration.
- Repositories: GenericRepository<T>, UnitOfWork as concrete DAL implementations.
- Entities: Employee, BaseEntity.
- Validators: CreateEmployeeDtoValidator, UpdateEmployeeDtoValidator (FluentValidation).
- Middleware/Configuration: Program.cs configures DI, EF Core, Swagger, AutoMapper, and FluentValidation.
- Mapping: MappingProfile (AutoMapper) maps DTOs <> Entities.
- Commands / Queries / Handlers: This project follows a service-based approach (no explicit CQRS commands/queries/handlers files present); `IEmployeeService` and `EmployeeService` serve as application handlers.

**API Endpoints Summary**

- Base route: /api/employees (controller: EmployeesController)

| Method | Path | Purpose | Request DTO | Response |
|---|---:|---|---|---|
| POST | /api/employees | Create employee | CreateEmployeeDto | EmployeeResponseDto |
| PUT | /api/employees | Update employee | UpdateEmployeeDto | EmployeeResponseDto |
| DELETE | /api/employees/{id} | Soft-delete (deactivate) employee | — | 200 OK / 404 |
| GET | /api/employees | Get all or by id (query) | ?id={guid} | List<EmployeeResponseDto> |
| POST | /api/employees/overtime | Calculate overtime pay | OvertimeRequestDto | { EmployeeId, Hours, OvertimePayment }

**Request flow / Execution flow**

1. HTTP request arrives at EmployeesController.
2. Controller validates model (FluentValidation via automatic registration).
3. Controller calls `IEmployeeService` (DI-injected `EmployeeService`).
4. `EmployeeService` maps DTOs to domain `Employee` via AutoMapper, uses `IUnitOfWork.Employees` (GenericRepository) to query or persist data.
5. For overtime calculation, `EmployeeService` uses `AbstractFactoryProvider` to select department-specific factories and `IOverTimePay` implementations.
6. `UnitOfWork` calls `AppDbContext.SaveChangesAsync()` to persist changes (EF Core -> SQL Server).

**Dependency Injection usage**

- All DI registrations occur in [Program.cs](Practical_23.API/Program.cs):
  - DbContext: `AddDbContext<AppDbContext>(options => UseSqlServer(...))`.
  - Generic repository: `IGenericRepository<> -> GenericRepository<>`.
  - UnitOfWork: `IUnitOfWork -> UnitOfWork`.
  - Service: `IEmployeeService -> EmployeeService`.
  - AutoMapper: `AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>())`.
  - FluentValidation: `AddFluentValidationAutoValidation()` and validators registration.
  - Factory classes and AbstractFactoryProvider: concrete factories and provider are registered as scoped services so they can be resolved by `EmployeeService`.

**Database usage**

- [Data/AppDbContext.cs](Practical_23.Infrastructure/Data/AppDbContext.cs) defines DbSet<Employee> and global query filter for soft deletes.
- Uses SQL Server connection string read from `appsettings.json` (`DefaultConnection`) registered in `Program.cs`.
- Migrations are present in [Practical_23.Infrastructure/Migrations](Practical_23.Infrastructure/Migrations).

**Mapping**

- [Mappings/MappingProfile.cs](Practical_23.Application/Mappings/MappingProfile.cs) contains AutoMapper configuration:
  - CreateEmployeeDto -> Employee
  - UpdateEmployeeDto -> Employee (with ignored members)
  - Employee -> EmployeeResponseDto (department mapped to string)

**SOLID principles followed**

- Single Responsibility: Controllers handle HTTP concerns; services handle business logic; repositories handle data access.
- Open/Closed: New overtime pay types can be added by creating new factories/products without modifying existing logic (Abstract Factory pattern).
- Liskov Substitution: Interfaces (`IOverTimePay`, `IGenericRepository<T>`, `IEmployeeService`) allow concrete implementations to be substituted.
- Interface Segregation: Small focused interfaces for repositories and services.
- Dependency Inversion: High-level modules (`EmployeeService`, `EmployeesController`) depend on abstractions (`IUnitOfWork`, `IEmployeeService`, `IGenericRepository<T>`) registered via DI.

**Notes / Next steps**

- Tests: No unit/integration tests present — adding tests around `EmployeeService`, factories and repositories is recommended.
- Secrets: Ensure connection strings are secured (user-secrets or environment variables) rather than committing to source.
- Consider adding explicit request/response contracts and OpenAPI documentation details (e.g., attributes, summaries) for clearer API docs.

---

If you'd like, I can:
- Generate a fully expanded file tree including build artifacts.
- Add a CONTRIBUTING or RUNNING section with exact run commands.
