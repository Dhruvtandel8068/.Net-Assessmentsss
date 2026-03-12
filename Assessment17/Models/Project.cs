namespace Assessment17.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    // Many-to-Many
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
}