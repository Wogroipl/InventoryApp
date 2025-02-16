using Repository.Core;

namespace Repository.Models;

public class ContactPerson : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;

    // Add more properties here

    // Navigation properties
    public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
}
