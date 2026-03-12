using Assessment17.Models;

namespace Assessment17.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project?> GetByIdWithEmployeesAsync(int id);

    Task AddAsync(Project project);
    void Update(Project project);
    void Delete(Project project);

    Task<bool> ExistsAsync(int id);

    // Many-to-Many operations
    Task<bool> AssignEmployeeAsync(int employeeId, int projectId);
    Task<bool> RemoveEmployeeAsync(int employeeId, int projectId);

    Task<bool> SaveAsync();
}