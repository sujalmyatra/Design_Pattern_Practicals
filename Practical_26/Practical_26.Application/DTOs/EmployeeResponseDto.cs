
namespace Practical_26.Application.DTOs
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public decimal Salary { get; set; }

        public string Department { get; set; } = String.Empty;

        public string EmailId { get; set; } = String.Empty;

        public DateTime JoiningDate { get; set; }

        public bool Status { get; set; }
    }
}
