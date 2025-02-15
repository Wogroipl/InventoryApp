using Repository.Core;

namespace Repository.Models;

public class Job : ModelBase
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime Loadin { get; set; }
    public DateTime Loadout { get; set; }
    public Guid CustomerId { get; set; }
    public Guid VenueId { get; set; }

    // Add more properties here

    // Navigation properties
    public Customer Customer { get; set; } = new();
    public Venue Venue { get; set; } = new();
    public IEnumerable<Transaction> Transactions { get; set; } = [];
}
