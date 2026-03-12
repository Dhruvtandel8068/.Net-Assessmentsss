namespace Assessment17.DTOs;

public record ProjectCreateDto(string Title);
public record ProjectUpdateDto(string Title);

public record ProjectReadDto(int Id, string Title);