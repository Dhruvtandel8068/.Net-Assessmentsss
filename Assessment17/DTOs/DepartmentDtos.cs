namespace Assessment17.DTOs;

public record DepartmentCreateDto(string Name);
public record DepartmentUpdateDto(string Name);

public record DepartmentReadDto(int Id, string Name);