using MediatR;
using Practical_25.Application.DTOs;

namespace Practical_25.Application.Query;

public class GetByEmployeeIdQuery : IRequest<EmployeeResponseDto>
{
    public Guid Id { get; set; }
}
