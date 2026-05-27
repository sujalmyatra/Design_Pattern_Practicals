using Practical_22.Application.Interfaces;
using Practical_22.Application.Mappings;
using Practical_22.Application.Services;
using Practical_22.Domain.Interfaces;
using Practical_22.Infrastructure.Data;
using Practical_22.Infrastructure.Repositories;
using Practical_22.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Practical_22.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies()
);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton<ILoggerService>(FileLogger.Instance);

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
