using Repository.Core;

namespace Repository.Models;

public class Venue : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? ZipCode { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;

    // Navigation properties
    public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    public IEnumerable<VenueHall> VenueHalls { get; set; } = new List<VenueHall>();
}
