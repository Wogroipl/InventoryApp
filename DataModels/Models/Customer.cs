namespace Repository.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Navigation properties for one-to-many relationships
    public IEnumerable<Job> Jobs { get; set; } = [];
}
