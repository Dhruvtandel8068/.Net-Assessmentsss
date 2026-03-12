using Assessment17.Data;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assessment17.Repositories.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;
    public EmployeeRepository(AppDbContext db) => _db = db;

    public Task<List<Employee>> GetAllWithDepartmentAsync() =>
        _db.Employees
          .Include(e => e.Department)
          .AsNoTracking()
          .ToListAsync();

    public Task<Employee?> GetByIdWithDepartmentAsync(int id) =>
        _db.Employees
          .Include(e => e.Department)
          .FirstOrDefaultAsync(e => e.Id == id);

    public Task<Employee?> GetByIdWithProjectsAsync(int id) =>
        _db.Employees
          .Include(e => e.Department)
          .Include(e => e.EmployeeProjects)
            .ThenInclude(ep => ep.Project)
          .FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddAsync(Employee employee) =>
        await _db.Employees.AddAsync(employee);

    public void Update(Employee employee) =>
        _db.Employees.Update(employee);

    public void Delete(Employee employee) =>
        _db.Employees.Remove(employee);

    public Task<bool> ExistsAsync(int id) =>
        _db.Employees.AnyAsync(e => e.Id == id);

    public Task<bool> EmailExistsAsync(string email, int? ignoreEmployeeId = null)
    {
        var query = _db.Employees.AsQueryable();
        if (ignoreEmployeeId.HasValue)
            query = query.Where(e => e.Id != ignoreEmployeeId.Value);

        return query.AnyAsync(e => e.Email == email);
    }

    public async Task<bool> SaveAsync() =>
        (await _db.SaveChangesAsync()) > 0;
}