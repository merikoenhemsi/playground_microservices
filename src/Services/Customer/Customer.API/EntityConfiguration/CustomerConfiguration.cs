using Customer.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.API.EntityConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<Entities.Customer>
{
    public void Configure(EntityTypeBuilder<Entities.Customer> builder)
    {
        builder.ToTable("customers", CustomerContext.DEFAULT_SCHEMA);

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
            .Property(o => o.FirstName)
            .HasColumnName("FirstName")
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(o => o.LastName)
            .HasColumnName("LastName")
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(o => o.Address)
            .HasColumnName("Address")
            .IsRequired(false)
            .HasMaxLength(250);

        builder
            .Property(o => o.Email)
            .HasColumnName("Email")
            .IsRequired(false)
            .HasMaxLength(100);

        builder
            .Property(o => o.Gender)
            .HasColumnName("Gender")
            .IsRequired(false)
            .HasMaxLength(10);

    }
}