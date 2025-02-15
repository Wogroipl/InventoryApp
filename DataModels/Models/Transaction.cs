using Repository.Core;

namespace Repository.Models;

public class Transaction : ModelBase
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid JobId { get; set; }

    // Add more properties here

    //Navigation properties
    public Job Job { get; set; } = new();
    public Product Product { get; set; } = new();

}
