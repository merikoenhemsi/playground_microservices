using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.EntityConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItemConfiguration)
    {

        orderItemConfiguration.ToTable("orderItems", OrderContext.DEFAULT_SCHEMA);

        orderItemConfiguration.HasKey(o => o.Id);

        orderItemConfiguration
            .Property(o => o.CreatedDate)
            .HasColumnName("CreatedDate")
            .IsRequired();

        orderItemConfiguration
            .Property(o => o.ModifiedDate)
            .HasColumnName("ModifiedDate")
            .IsRequired(false);

        orderItemConfiguration.Property(o=>o.MasterId)
            .IsRequired().HasMaxLength(10);

        orderItemConfiguration.Property(o => o.ProductId)
            .IsRequired().HasMaxLength(10);

        orderItemConfiguration.Property(o => o.ProductName)
            .IsRequired().HasMaxLength(250);

        orderItemConfiguration.Property(o => o.Count)
            .IsRequired().HasMaxLength(250);

        orderItemConfiguration.Property(oi => oi.Price)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");
    }
}