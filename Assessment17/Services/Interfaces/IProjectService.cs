using Assessment17.DTOs;

namespace Assessment17.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectReadDto>> GetAllAsync();
        Task<ProjectReadDto?> GetByIdAsync(int id);
        Task<ProjectReadDto> CreateAsync(ProjectCreateDto dto);
        Task<bool> UpdateAsync(int id, ProjectUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<bool> AssignEmployeeAsync(AssignEmployeeProjectDto dto);
        Task<bool> RemoveEmployeeAsync(AssignEmployeeProjectDto dto);

        Task<object?> GetProjectEmployeesAsync(int projectId);
    }
}