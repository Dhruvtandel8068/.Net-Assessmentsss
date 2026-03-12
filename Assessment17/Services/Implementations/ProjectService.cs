using AutoMapper;
using Assessment17.DTOs;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Assessment17.Services.Interfaces;

namespace Assessment17.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projRepo;
    private readonly IEmployeeRepository _empRepo;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projRepo, IEmployeeRepository empRepo, IMapper mapper)
    {
        _projRepo = projRepo;
        _empRepo = empRepo;
        _mapper = mapper;
    }

    public async Task<List<ProjectReadDto>> GetAllAsync()
    {
        var list = await _projRepo.GetAllAsync();
        return _mapper.Map<List<ProjectReadDto>>(list);
    }

    public async Task<ProjectReadDto?> GetByIdAsync(int id)
    {
        var proj = await _projRepo.GetByIdAsync(id);
        return proj is null ? null : _mapper.Map<ProjectReadDto>(proj);
    }

    public async Task<ProjectReadDto> CreateAsync(ProjectCreateDto dto)
    {
        var entity = _mapper.Map<Project>(dto);
        await _projRepo.AddAsync(entity);
        await _projRepo.SaveAsync();
        return _mapper.Map<ProjectReadDto>(entity);
    }

    public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
    {
        var proj = await _projRepo.GetByIdAsync(id);
        if (proj is null) return false;

        _mapper.Map(dto, proj);
        _projRepo.Update(proj);
        return await _projRepo.SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var proj = await _projRepo.GetByIdAsync(id);
        if (proj is null) return false;

        _projRepo.Delete(proj);
        return await _projRepo.SaveAsync();
    }

    public async Task<bool> AssignEmployeeAsync(AssignEmployeeProjectDto dto)
    {
        if (!await _empRepo.ExistsAsync(dto.EmployeeId))
            throw new InvalidOperationException("Employee not found.");

        if (!await _projRepo.ExistsAsync(dto.ProjectId))
            throw new InvalidOperationException("Project not found.");

        var assigned = await _projRepo.AssignEmployeeAsync(dto.EmployeeId, dto.ProjectId);
        if (!assigned) return false;

        return await _projRepo.SaveAsync();
    }

    public async Task<bool> RemoveEmployeeAsync(AssignEmployeeProjectDto dto)
    {
        var removed = await _projRepo.RemoveEmployeeAsync(dto.EmployeeId, dto.ProjectId);
        if (!removed) return false;

        return await _projRepo.SaveAsync();
    }

    public async Task<object?> GetProjectEmployeesAsync(int projectId)
    {
        var project = await _projRepo.GetByIdWithEmployeesAsync(projectId);
        if (project is null) return null;

        var employees = project.EmployeeProjects
            .Where(ep => ep.Employee != null)
            .Select(ep => new
            {
                ep.EmployeeId,
                fullName = ep.Employee!.FullName,
                email = ep.Employee!.Email,
                department = ep.Employee!.Department?.Name ?? "N/A",
                ep.AssignedOn
            })
            .ToList();

        return new
        {
            projectId = project.Id,
            title = project.Title,
            employees
        };
    }
}