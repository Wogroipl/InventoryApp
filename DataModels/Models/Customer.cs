using Repository.Core;

namespace Repository.Models;

public class Customer : ModelBase
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    //Add more properties here

    // Navigation properties for one-to-many relationships
    public IEnumerable<Job> Jobs { get; set; } = [];
}
