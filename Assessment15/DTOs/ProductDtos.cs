namespace Assessment15.DTOs;

public record ProductCreateDto(string Name, decimal Price);
public record ProductUpdateDto(string Name, decimal Price);