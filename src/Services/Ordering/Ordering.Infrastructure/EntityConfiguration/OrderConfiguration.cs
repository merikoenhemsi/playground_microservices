using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.ToTable("orders", OrderContext.DEFAULT_SCHEMA);

        orderConfiguration.HasKey(o => o.Id);

        orderConfiguration
            .Property(o => o.CreatedDate)
            .HasColumnName("CreatedDate")
            .IsRequired();

        orderConfiguration
            .Property(o => o.ModifiedDate)
            .HasColumnName("ModifiedDate")
            .IsRequired(false);

        orderConfiguration
            .Property(o=>o.CustomerId)
            .HasColumnName("CustomerId")
            .IsRequired(false)
            .HasMaxLength(10);

        orderConfiguration
            .Property(o => o.CustomerName)
            .HasColumnName("CustomerName")
            .IsRequired(false)
            .HasMaxLength(250);

        orderConfiguration
            .Property(o => o.OrderStatus)
            .HasColumnName("OrderStatus")
            .IsRequired();

        orderConfiguration.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(item => item.MasterId);

    }
}