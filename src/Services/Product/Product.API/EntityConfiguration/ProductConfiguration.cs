using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Data;

namespace Product.API.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Entities.Product>
{
    public void Configure(EntityTypeBuilder<Entities.Product> builder)
    {
        builder.ToTable("products", ProductContext.DEFAULT_SCHEMA);

        builder.HasKey(o => o.Id);

        builder
            .Property(o => o.CreatedDate)
            .HasColumnName("CreatedDate")
            .IsRequired();

        builder
            .Property(o => o.ModifiedDate)
            .HasColumnName("ModifiedDate")
            .IsRequired(false);

        builder
            .Property(o => o.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(o => o.Description)
            .HasColumnName("Description")
            .IsRequired(false)
            .HasMaxLength(150);

        builder
            .Property(o => o.Price)
            .HasColumnName("Price")
            .IsRequired()
            .HasPrecision(18, 2);
    }
}