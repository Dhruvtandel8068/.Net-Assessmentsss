using Assessment17.DTOs;
using Assessment17.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assessment17.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    public ProjectsController(IProjectService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proj = await _service.GetByIdAsync(id);
        return proj is null ? NotFound("Project not found.") : Ok(proj);
    }

    [HttpGet("{id:int}/employees")]
    public async Task<IActionResult> GetProjectEmployees(int id)
    {
        var data = await _service.GetProjectEmployeesAsync(id);
        return data is null ? NotFound("Project not found.") : Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProjectUpdateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? Ok("Project updated.") : NotFound("Project not found.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? Ok("Project deleted.") : NotFound("Project not found.");
    }

    // Many-to-Many: Assign employee to project
    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignEmployeeProjectDto dto)
    {
        var ok = await _service.AssignEmployeeAsync(dto);
        return ok ? Ok("Employee assigned to project.") : BadRequest("Already assigned.");
    }

    // Many-to-Many: Remove employee from project
    [HttpPost("remove")]
    public async Task<IActionResult> Remove(AssignEmployeeProjectDto dto)
    {
        var ok = await _service.RemoveEmployeeAsync(dto);
        return ok ? Ok("Employee removed from project.") : BadRequest("Assignment not found.");
    }
}