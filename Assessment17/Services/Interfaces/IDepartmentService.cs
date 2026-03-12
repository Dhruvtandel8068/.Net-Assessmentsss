using Assessment17.DTOs;

namespace Assessment17.Services.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentReadDto>> GetAllAsync();
    Task<DepartmentReadDto?> GetByIdAsync(int id);
    Task<DepartmentReadDto> CreateAsync(DepartmentCreateDto dto);
    Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}