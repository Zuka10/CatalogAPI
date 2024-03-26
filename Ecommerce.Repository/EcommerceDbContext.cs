using Ecommerce.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository;

public class EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.CategoryId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId);

        modelBuilder.Entity<Image>()
            .HasOne(c => c.Product)
            .WithMany(c => c.Images)
            .HasForeignKey(c => c.ProductId);

        modelBuilder.Entity<Product>()
            .HasMany(c => c.Images)
            .WithOne(c => c.Product)
            .HasForeignKey(c => c.ProductId);

        base.OnModelCreating(modelBuilder);
    }
}