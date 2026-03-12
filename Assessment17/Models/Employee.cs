namespace Assessment17.Models;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Salary { get; set; }

    // One-to-Many (Department)
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    // Many-to-Many
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
}