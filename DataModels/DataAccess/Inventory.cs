using Repository.Models;

namespace Repository.DataAccess;

// Inventory class to hold all the data, temporary solution until we implement a database
public class Inventory
{
    //profucts table
    public List<Product> Products { get; set; }
    //customers table
    public List<Customer> Customers { get; set; }
    //venues table
    public List<Venue> Venues { get; set; }
    //jobs table
    public List<Job> Jobs { get; set; }
    //transactions table
    public List<Transaction> Transactions { get; set; }

    /// <summary>
    /// Constructor to initialize the Inventory with some data
    /// </summary>
    public Inventory()
    {
        Products =
            [
                new Product
                {
                    Id = Guid.CreateVersion7(),
                    Code = "P001",
                    Name = "Product 1",
                    Stock = 100,
                    KeepTrackOfStock = true,
                },
                new Product
                {
                    Id = Guid.CreateVersion7(),
                    Code = "P002",
                    Name = "Product 2",
                    Stock = 200,
                    KeepTrackOfStock = true,
                }
            ];

        Customers =
            [
                new Customer
                {
                    Id = Guid.CreateVersion7(),
                    Code = "C001",
                    Name = "Customer 1",
                },
                new Customer
                {
                    Id = Guid.CreateVersion7(),
                    Code = "C002",
                    Name = "Customer 2",
                }
            ];

        Venues =
            [
                new Venue
                {
                    Id = Guid.CreateVersion7(),
                    Code = "L001",
                    Name = "Location 1",
                },
                new Venue
                {
                    Id = Guid.CreateVersion7(),
                    Code = "L002",
                    Name = "Location 2",
                }
            ];

        Jobs =
            [
                new Job
                {
                    Id = Guid.CreateVersion7(),
                    Name = $"{Customers[0].Name} {Venues[0].Name}",
                    CustomerId = Customers[0].Id,
                    VenueId = Venues[0].Id,
                    Loadin = DateTime.Now.AddDays(5),
                    Loadout = DateTime.Now.AddDays(8)
                },
                new Job
                {
                    Id = Guid.CreateVersion7(),
                    Name = $"{Customers[1].Name} {Venues[1].Name}",
                    CustomerId = Customers[1].Id,
                    VenueId = Venues[1].Id,
                    Loadin = DateTime.Now.AddDays(7),
                    Loadout = DateTime.Now.AddDays(9),
                }
            ];

        Transactions =
            [
                new Transaction
                {
                    Id = Guid.CreateVersion7(),
                    JobId = Jobs[0].Id,
                    ProductId = Products[0].Id,
                    Quantity = 12,
                },
                new Transaction
                {
                    Id = Guid.CreateVersion7(),
                    JobId = Jobs[1].Id,
                    ProductId = Products[1].Id,
                    Quantity = 10,
                }
            ];
    }
}
