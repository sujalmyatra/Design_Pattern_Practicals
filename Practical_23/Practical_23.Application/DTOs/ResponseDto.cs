using Practical_23.Domain.Enums;

namespace Practical_23.Application.DTOs;

public class ResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public string Department { get; set; } = string.Empty;

    public string EmailId { get; set; } = string.Empty;

    public DateTime JoiningDate { get; set; }

    public bool Status { get; set; }
}
