using Assessment17.Models;

namespace Assessment17.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllWithDepartmentAsync();
    Task<Employee?> GetByIdWithDepartmentAsync(int id);
    Task<Employee?> GetByIdWithProjectsAsync(int id);

    Task AddAsync(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);

    Task<bool> ExistsAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? ignoreEmployeeId = null);

    Task<bool> SaveAsync();
}