using Repository.Core;

namespace Repository.Models;

public class Product : ModelBase
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public bool KeepTrackOfStock { get; set; }

    //Add more properties here

    //Navigation properties
    public IEnumerable<Transaction> Transactions { get; set; } = [];
}
