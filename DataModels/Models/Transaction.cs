using Repository.Core;

namespace Repository.Models;

public class Transaction : ModelBase
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid JobId { get; set; }

    // Navigation properties
    public Product Product { get; set; } = new();
    public Job Job { get; set; } = new();


}
