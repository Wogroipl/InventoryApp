namespace Repository.Models;

public class Category
{
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public IEnumerable<Product> Products { get; set; } = [];
}
