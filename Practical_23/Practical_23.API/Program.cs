using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Practical_23.Application.Abstract_Factory;
using Practical_23.Application.Abstract_Factory.Interfaces;
using Practical_23.Application.Factories;
using Practical_23.Application.Factories.Interfaces;
using Practical_23.Application.Interfaces;
using Practical_23.Application.Mappings;
using Practical_23.Application.Services;
using Practical_23.Application.Validators;
using Practical_23.Domain.Entities;
using Practical_23.Domain.Interfaces;
using Practical_23.Infrastructure.Data;
using Practical_23.Infrastructure.Repositories;

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
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


builder.Services.AddScoped<IOverTimePayFactory, ITOverTimePayFactory>();

builder.Services.AddScoped<IOverTimePayFactory, HROverTimePayFactory>();

builder.Services.AddScoped<IOverTimePayFactory, SalesOverTimePayFactory>();

builder.Services.AddScoped<IOverTimePayFactory, OnSiteOverTimePayFactory>();

builder.Services.AddScoped<IOverTimePayAbstractFactory, IndoorFactory>();

builder.Services.AddScoped<IOverTimePayAbstractFactory, OutdoorFactory>();

builder.Services.AddScoped<AbstractFactoryProvider>();

builder.Services.AddScoped<ITOverTimePayFactory>();

builder.Services.AddScoped<HROverTimePayFactory>();

builder.Services.AddScoped<SalesOverTimePayFactory>();

builder.Services.AddScoped<OnSiteOverTimePayFactory>();


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
