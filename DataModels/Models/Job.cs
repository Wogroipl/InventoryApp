namespace Repository.Models;

public class Job
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Loadin { get; set; }
    public DateTime Loadout { get; set; }
    public Guid CustomerId { get; set; }
    public Guid VenueId { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public Venue Venue { get; set; } = null!;
    public IEnumerable<Transaction> Transactions { get; set; } = [];
}
