using System.ComponentModel.DataAnnotations;

namespace Assessment18.Models.Entities;

public class AccidentReport
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public int? GeoLocationId { get; set; }
    public GeoLocation? GeoLocation { get; set; }

    [Required]
    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Notes { get; set; }

    // Navigation
    public ICollection<AccidentPhoto> Photos { get; set; } = new List<AccidentPhoto>();
    public ICollection<NotificationLog> Notifications { get; set; } = new List<NotificationLog>();
}