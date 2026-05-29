using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Practical_25.Domain.Interfaces;
using Practical_25.Application.Mappings;
using Practical_25.Application.Validators;
using Practical_25.Infrastructure.Data;
using Practical_25.Infrastructure.Repositories;
using Practical_25.Application.handlers;

namespace Practical_25.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();



        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped(typeof(IGenericCommandRepository<>), typeof(GenericCommandRepository<>));

        builder.Services.AddScoped(typeof(IGenericQueryRepository<>), typeof(GenericQueryRepository<>));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

        builder.Services.AddMediatR(x => { x.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly); });

        builder.Services.AddValidatorsFromAssembly(typeof(CreateEmployeeCommandValidator).Assembly);

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
