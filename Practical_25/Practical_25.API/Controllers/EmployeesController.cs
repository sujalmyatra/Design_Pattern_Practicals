using Microsoft.AspNetCore.Mvc;
using MediatR;
using Practical_25.Application.Command;
using Practical_25.Application.Query;

namespace Practical_25.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand cmd)
    {
        var result = await _mediator.Send(cmd);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeCommand cmd)
    {
        var result = await _mediator.Send(cmd);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id});

        if(result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id)
    {
        if (id.HasValue)
        {
            var employee = await _mediator.Send(new GetByEmployeeIdQuery { Id = id.Value });

            return Ok(employee);
        }

        var employees = await _mediator.Send(new GetAllEmployeesQuery());

        return Ok(employees);
    }
}