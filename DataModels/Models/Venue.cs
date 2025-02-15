using Repository.Core;

namespace Repository.Models;

public class Venue : ModelBase
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Add more properties here

    // Navigation properties
    public IEnumerable<Job> Jobs { get; set; } = [];
}
