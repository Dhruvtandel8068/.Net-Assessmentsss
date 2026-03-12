namespace Assessment17.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // One-to-Many
    public List<Employee> Employees { get; set; } = new();
}