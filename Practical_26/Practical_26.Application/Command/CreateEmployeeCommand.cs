using Practical_26.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practical_26.Application.Command
{
    public class CreateEmployeeCommand
    {
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string EmailId { get; set; } = string.Empty;
    }
}
