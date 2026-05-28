using Practical_26.Application.Validators;
using Practical_26.Application.Services;
using Practical_26.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Practical_26.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Practical_26.Application.Interfaces;
using Practical_26.Domain.Interfaces;
using Practical_26.Domain.Entities;
using Practical_26.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeCommandValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
);

builder.Services.AddScoped<IEmployeeCommandService, EmployeeCommandService>();
builder.Services.AddScoped<IEmployeeQueryService, EmployeeQueryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGenericCommandRepository<Employee>, GenericCommandRepository<Employee>>();


builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
