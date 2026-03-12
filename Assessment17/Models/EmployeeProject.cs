namespace Assessment17.Models;

public class EmployeeProject
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
}