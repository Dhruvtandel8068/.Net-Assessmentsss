namespace Assessment17.DTOs;

public record EmployeeCreateDto(string FullName, string Email, decimal Salary, int DepartmentId);
public record EmployeeUpdateDto(string FullName, string Email, decimal Salary, int DepartmentId);

public record EmployeeReadDto(int Id, string FullName, string Email, decimal Salary, int DepartmentId, string DepartmentName);