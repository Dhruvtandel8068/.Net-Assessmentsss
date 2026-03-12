using Assessment17.Data;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assessment17.Repositories.Implementations;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;
    public ProjectRepository(AppDbContext db) => _db = db;

    public Task<List<Project>> GetAllAsync() =>
        _db.Projects.AsNoTracking().ToListAsync();

    public Task<Project?> GetByIdAsync(int id) =>
        _db.Projects.FirstOrDefaultAsync(p => p.Id == id);

    public Task<Project?> GetByIdWithEmployeesAsync(int id) =>
        _db.Projects
          .Include(p => p.EmployeeProjects)
            .ThenInclude(ep => ep.Employee)
              .ThenInclude(e => e!.Department)
          .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Project project) =>
        await _db.Projects.AddAsync(project);

    public void Update(Project project) =>
        _db.Projects.Update(project);

    public void Delete(Project project) =>
        _db.Projects.Remove(project);

    public Task<bool> ExistsAsync(int id) =>
        _db.Projects.AnyAsync(p => p.Id == id);

    public async Task<bool> AssignEmployeeAsync(int employeeId, int projectId)
    {
        var exists = await _db.EmployeeProjects.AnyAsync(ep =>
            ep.EmployeeId == employeeId && ep.ProjectId == projectId);

        if (exists) return false;

        await _db.EmployeeProjects.AddAsync(new EmployeeProject
        {
            EmployeeId = employeeId,
            ProjectId = projectId,
            AssignedOn = DateTime.UtcNow
        });

        return true;
    }

    public async Task<bool> RemoveEmployeeAsync(int employeeId, int projectId)
    {
        var link = await _db.EmployeeProjects.FirstOrDefaultAsync(ep =>
            ep.EmployeeId == employeeId && ep.ProjectId == projectId);

        if (link is null) return false;

        _db.EmployeeProjects.Remove(link);
        return true;
    }

    public async Task<bool> SaveAsync() =>
        (await _db.SaveChangesAsync()) > 0;
}