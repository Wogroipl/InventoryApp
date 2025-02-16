using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace InventoryBlazorHybrid.DataAccess;

public class InventoryDbContext : DbContext
{
    DbSet<Job> Jobs { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Venue> Venues { get; set; }
    DbSet<VenueHall> VenueHalls { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<ContactPerson> Contacts { get; set; }


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
