using Assessment17.DTOs;
using Assessment17.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assessment17.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _service;
    public DepartmentsController(IDepartmentService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dept = await _service.GetByIdAsync(id);
        return dept is null ? NotFound("Department not found.") : Ok(dept);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentUpdateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? Ok("Department updated.") : NotFound("Department not found.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? Ok("Department deleted.") : NotFound("Department not found.");
    }
}