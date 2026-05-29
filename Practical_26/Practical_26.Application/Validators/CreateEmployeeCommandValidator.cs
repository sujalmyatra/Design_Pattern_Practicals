using FluentValidation;
using Practical_26.Application.Command;

namespace Practical_26.Application.Validators;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name is Required")
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(e => e.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0");
        RuleFor(e => e.DepartmentId)
            .InclusiveBetween(1, 5)
            .WithMessage("Invalid Department");
        RuleFor(e => e.EmailId)
           .NotEmpty()
           .EmailAddress()
           .WithMessage("Invalid Email Address");
    }
}
