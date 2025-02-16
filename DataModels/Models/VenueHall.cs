using Repository.Core;

namespace Repository.Models;

public class VenueHall : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public Guid VenueId { get; set; }

    // Navigation properties
    public Venue Venue { get; set; } = new Venue();
}
