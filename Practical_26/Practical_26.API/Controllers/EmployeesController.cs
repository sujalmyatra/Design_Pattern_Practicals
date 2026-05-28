using Practical_26.Application.DTOs;
using Practical_26.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Practical_26.Application.Command;

namespace Practical_26.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeQueryService _queryService;
        private readonly IEmployeeCommandService _commandService;

        public EmployeesController(IEmployeeQueryService queryService, IEmployeeCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var response = await _commandService.CreateEmployeeAsync(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeCommand command)
        {
            var response = await _commandService.UpdateEmployeeAsync(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _commandService.DeleteEmployeeAsync(id);

            if (!response)
                return NotFound();

            return Ok("Employee Deactivated");
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? id)
        {
            var response = await _queryService.GetEmployeesAsync(id);

            return Ok(response);
        }

    }
}