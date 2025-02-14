namespace Repository.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid JobId { get; set; }

    //Navigation properties
    public Job Job { get; set; } = null!;
    public Product Product { get; set; } = null!;

}
