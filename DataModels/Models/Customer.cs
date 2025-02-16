using Repository.Core;

namespace Repository.Models;

public class Customer : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? ZipCode { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;

    //Add more properties here

    // Navigation properties
    public IEnumerable<ContactPerson> Contacts { get; set; } = new List<ContactPerson>();
    public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
}
