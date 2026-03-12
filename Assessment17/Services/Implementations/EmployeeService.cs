using Assessment17.DTOs;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Assessment17.Services.Interfaces;

namespace Assessment17.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _empRepo;
    private readonly IDepartmentRepository _deptRepo;

    public EmployeeService(IEmployeeRepository empRepo, IDepartmentRepository deptRepo)
    {
        _empRepo = empRepo;
        _deptRepo = deptRepo;
    }

    public async Task<List<EmployeeReadDto>> GetAllAsync()
    {
        var employees = await _empRepo.GetAllWithDepartmentAsync();
        return employees.Select(ToReadDto).ToList();
    }

    public async Task<EmployeeReadDto?> GetByIdAsync(int id)
    {
        var emp = await _empRepo.GetByIdWithDepartmentAsync(id);
        return emp is null ? null : ToReadDto(emp);
    }

    public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto)
    {
        // Validate Department
        var dept = await _deptRepo.GetByIdAsync(dto.DepartmentId);
        if (dept is null)
            throw new InvalidOperationException("Department not found.");

        // Unique Email
        if (await _empRepo.EmailExistsAsync(dto.Email))
            throw new InvalidOperationException("Email already exists.");

        var employee = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId
        };

        await _empRepo.AddAsync(employee);
        await _empRepo.SaveAsync();

        // reload with department for DepartmentName
        var created = await _empRepo.GetByIdWithDepartmentAsync(employee.Id);
        return created is null ? ToReadDto(employee, dept.Name) : ToReadDto(created);
    }

    public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto)
    {
        var emp = await _empRepo.GetByIdWithDepartmentAsync(id);
        if (emp is null) return false;

        var dept = await _deptRepo.GetByIdAsync(dto.DepartmentId);
        if (dept is null)
            throw new InvalidOperationException("Department not found.");

        if (await _empRepo.EmailExistsAsync(dto.Email, ignoreEmployeeId: id))
            throw new InvalidOperationException("Email already exists.");

        emp.FullName = dto.FullName;
        emp.Email = dto.Email;
        emp.Salary = dto.Salary;
        emp.DepartmentId = dto.DepartmentId;

        _empRepo.Update(emp);
        return await _empRepo.SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var emp = await _empRepo.GetByIdWithDepartmentAsync(id);
        if (emp is null) return false;

        _empRepo.Delete(emp);
        return await _empRepo.SaveAsync();
    }

    public async Task<List<EmployeeReadDto>> GetByDepartmentAsync(int departmentId)
    {
        var all = await _empRepo.GetAllWithDepartmentAsync();
        return all.Where(e => e.DepartmentId == departmentId)
                  .Select(ToReadDto)
                  .ToList();
    }

    public async Task<object?> GetEmployeeProjectsAsync(int employeeId)
    {
        var emp = await _empRepo.GetByIdWithProjectsAsync(employeeId);
        if (emp is null) return null;

        var projects = emp.EmployeeProjects
            .Where(ep => ep.Project != null)
            .Select(ep => new
            {
                ep.ProjectId,
                Title = ep.Project!.Title,
                ep.AssignedOn
            })
            .ToList();

        return new
        {
            employeeId = emp.Id,
            emp.FullName,
            emp.Email,
            department = emp.Department?.Name,
            projects
        };
    }

    private static EmployeeReadDto ToReadDto(Employee e)
        => new(
            e.Id,
            e.FullName,
            e.Email,
            e.Salary,
            e.DepartmentId,
            e.Department?.Name ?? "N/A"
        );

    private static EmployeeReadDto ToReadDto(Employee e, string deptName)
        => new(
            e.Id,
            e.FullName,
            e.Email,
            e.Salary,
            e.DepartmentId,
            deptName
        );
}