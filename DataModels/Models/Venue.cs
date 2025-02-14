namespace Repository.Models;

public class Venue
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public IEnumerable<Job> Jobs { get; set; } = [];
}
