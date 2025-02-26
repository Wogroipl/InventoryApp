using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.DataAccess;

public class InventoryDbContext : DbContext
{
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<VenueHall> VenueHalls { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactPerson> Contacts { get; set; }


    /// <summary>
    /// Configures the relationships between the entities
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //one-to many relationship between Job and Transaction
        modelBuilder.Entity<Job>()
            .HasMany(j => j.Transactions)
            .WithOne(t => t.Job)
            .HasForeignKey(t => t.JobId);

        //one-to-many relationship between Job and Venue
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Venue)
            .WithMany(v => v.Jobs)
            .HasForeignKey(j => j.VenueId);

        //one-to-many relationship between Job and Customer
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Customer)
            .WithMany(c => c.Jobs)
            .HasForeignKey(j => j.CustomerId);

        //many-to-many relationship between Customer and ContactPerson
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Contacts)
            .WithMany(cp => cp.Customers)
            .UsingEntity(j => j.ToTable("CustomerContacts"));

        //one-to-many relationship between Product and Transaction
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Transactions)
            .WithOne(t => t.Product)
            .HasForeignKey(t => t.ProductId);

        //one-to-many relationship between Product and Category
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        //one-to-many relationship between Venue and VenueHall
        modelBuilder.Entity<Venue>()
            .HasMany(v => v.VenueHalls)
            .WithOne(vh => vh.Venue)
            .HasForeignKey(vh => vh.VenueId);

    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }

}
