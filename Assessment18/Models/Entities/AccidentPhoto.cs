using System.ComponentModel.DataAnnotations;

namespace Assessment18.Models.Entities;

public class AccidentPhoto
{
    public int Id { get; set; }

    public int AccidentReportId { get; set; }
    public AccidentReport? AccidentReport { get; set; }

    [Required, MaxLength(400)]
    public string FileUrl { get; set; } = string.Empty;

    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
}