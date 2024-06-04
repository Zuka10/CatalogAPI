using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(c => c.OrderDetails)
            .WithOne(c => c.Order)
            .HasForeignKey(c => c.OrderId);

        builder.Property(p => p.TotalAmount)
            .HasColumnType("money");
    }
}