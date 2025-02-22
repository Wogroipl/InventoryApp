using Repository.Core;

namespace Repository.Models;

public class Job : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public DateTime Loadin { get; set; }
    public DateTime Loadout { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid VenueId { get; set; }
    public Guid? VenueHallId { get; set; }

    // Add more properties here

    // Navigation properties
    public Customer Customer { get; set; } = new();
    public ContactPerson Contact { get; set; } = new();
    public Venue Venue { get; set; } = new();
    public VenueHall VenueHall { get; set; } = new();
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
}
