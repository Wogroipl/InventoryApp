using Repository.Core;

namespace Repository.Models;

public class Category : ModelBase
{
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public IEnumerable<Product> Products { get; set; } = [];
}
