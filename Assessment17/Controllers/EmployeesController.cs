using Assessment17.DTOs;
using Assessment17.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assessment17.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;
    public EmployeesController(IEmployeeService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var emp = await _service.GetByIdAsync(id);
        return emp is null ? NotFound("Employee not found.") : Ok(emp);
    }

    [HttpGet("by-department/{departmentId:int}")]
    public async Task<IActionResult> GetByDepartment(int departmentId)
        => Ok(await _service.GetByDepartmentAsync(departmentId));

    [HttpGet("{id:int}/projects")]
    public async Task<IActionResult> GetEmployeeProjects(int id)
    {
        var data = await _service.GetEmployeeProjectsAsync(id);
        return data is null ? NotFound("Employee not found.") : Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? Ok("Employee updated.") : NotFound("Employee not found.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? Ok("Employee deleted.") : NotFound("Employee not found.");
    }
}