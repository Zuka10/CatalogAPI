using Catalog.Domain;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasColumnType("money");

        modelBuilder.Entity<OrderDetail>()
            .HasOne(c => c.Order)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(c => c.OrderId);

        modelBuilder.Entity<Order>()
            .HasMany(c => c.OrderDetails)
            .WithOne(c => c.Order)
            .HasForeignKey(c => c.OrderId);

        modelBuilder.Entity<Order>()
            .Property(p => p.TotalAmount)
            .HasColumnType("money");

        modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}