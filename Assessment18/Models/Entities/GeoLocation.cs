using System.ComponentModel.DataAnnotations;

namespace Assessment18.Models.Entities;

public class GeoLocation
{
    public int Id { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [MaxLength(250)]
    public string? AddressLine { get; set; }

    [MaxLength(80)]
    public string? City { get; set; }

    [MaxLength(80)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }
}