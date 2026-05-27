using System;
using System.Collections.Generic;
using System.Text;

namespace Practical_22.Application.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = String.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string EmailId { get; set; } = String.Empty;
    }
}
