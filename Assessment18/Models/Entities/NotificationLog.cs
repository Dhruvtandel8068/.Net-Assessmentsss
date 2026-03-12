using System.ComponentModel.DataAnnotations;
using Assessment18.Models.Enums;

namespace Assessment18.Models.Entities;

public class NotificationLog
{
    public int Id { get; set; }

    public int AccidentReportId { get; set; }
    public AccidentReport? AccidentReport { get; set; }

    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

    [Required, MaxLength(120)]
    public string Recipient { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Message { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}