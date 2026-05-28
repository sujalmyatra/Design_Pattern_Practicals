using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practical_23.Application.DTOs;
using Practical_23.Application.Interfaces;

namespace Practical_23.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmpService _empService;
    public EmployeeController(IEmpService empService)
    {
        _empService = empService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDto dto)
    {
        await _empService.AddAsync(dto);
        return Ok("Emp Created");
    }
    [HttpPatch]
    public async Task<IActionResult> Update(UpdateDto dto)
    {
        await _empService.UpdateAsync(dto);
        return Ok("Emp update");
    }
    [HttpDelete ("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _empService.DeleteAsync(id);
        return Ok("Emp Deactivate");
    }
    [HttpGet]
    public async Task<IActionResult> Get(Guid? id)
    {
        if (id.HasValue)
            return Ok(await  _empService.GetByIdAsync(id.Value));

        return Ok(await _empService.GetAllAsync());
    }
}
