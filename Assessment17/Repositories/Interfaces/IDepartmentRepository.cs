using Assessment17.Models;

namespace Assessment17.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task AddAsync(Department dept);
    void Update(Department dept);
    void Delete(Department dept);
    Task<bool> SaveAsync();
}