using Assessment17.Data;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assessment17.Repositories.Implementations;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _db;
    public DepartmentRepository(AppDbContext db) => _db = db;

    public Task<List<Department>> GetAllAsync() =>
        _db.Departments.AsNoTracking().ToListAsync();

    public Task<Department?> GetByIdAsync(int id) =>
        _db.Departments.FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Department dept) => await _db.Departments.AddAsync(dept);

    public void Update(Department dept) => _db.Departments.Update(dept);

    public void Delete(Department dept) => _db.Departments.Remove(dept);

    public async Task<bool> SaveAsync() => (await _db.SaveChangesAsync()) > 0;
}