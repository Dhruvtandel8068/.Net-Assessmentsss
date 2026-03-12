using Assessment17.DTOs;

namespace Assessment17.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeReadDto>> GetAllAsync();
        Task<EmployeeReadDto?> GetByIdAsync(int id);
        Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto);
        Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<List<EmployeeReadDto>> GetByDepartmentAsync(int departmentId);
        Task<object?> GetEmployeeProjectsAsync(int employeeId);
    }
}