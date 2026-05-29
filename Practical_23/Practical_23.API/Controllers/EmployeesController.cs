using Practical_23.Application.DTOs;
using Practical_23.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Practical_23.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var response = await _service.CreateEmployeeAsync(dto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeDto dto)
        {
            var response = await _service.UpdateEmployeeAsync(dto);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.DeleteEmployeeAsync(id);

            if(!response)
                return NotFound();
            
            return Ok("Employee Deactivated");
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? id)
        {
            var response = await _service.GetEmployeesAsync(id);

            return Ok(response);
        }

        [HttpPost("overtime")]
        public async Task<IActionResult> GetOvertime(OvertimeRequestDto dto)
        {
            var response = await _service.GetOverTimePayAsync(dto);

            return Ok(new
            {
                EmployeeId = dto.Id,
                Hours = dto.Hours,
                OvertimePayment = response
            });
        }
    }
}