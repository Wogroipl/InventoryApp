using Repository.Core;

namespace Repository.Models;

public class Product : ModelBase
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double? Weight { get; set; }
    public int? Stock { get; set; }
    public bool KeepTrackOfStock { get; set; }
    public Guid CategoryId { get; set; }

    //Navigation properties
    public Category Category { get; set; } = new Category();
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
}
