using FluentValidation;
using Practical_23.Application.DTOs;

namespace Practical_23.Application.Validator;

public class CreateValidator : AbstractValidator<CreateDto>
{
    public CreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Enter Name");

        RuleFor(x => x.EmailId).NotEmpty().EmailAddress().WithMessage("Enter valid email");

        RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Enter valid salary");
    }
}
