using System.ComponentModel.DataAnnotations;

namespace Assessment18.Models.Entities;

public class AppUser
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(180)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    // Navigation
    public ICollection<AccidentReport> AccidentReports { get; set; } = new List<AccidentReport>();
    public ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
}