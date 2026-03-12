using System.ComponentModel.DataAnnotations;

namespace Assessment18.Models.Entities;

public class EmergencyContact
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public AppUser? User { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Relationship { get; set; } // Father, Friend, etc.
}