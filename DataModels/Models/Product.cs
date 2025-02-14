namespace Repository.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public bool KeepTrackOfStock { get; set; }

    //Navigation properties
    public IEnumerable<Transaction> Transactions { get; set; } = [];
}
