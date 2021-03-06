// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Infrastructure.Data;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ordering.Core.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<int?>("CustomerId")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("CustomerName");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedDate");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int")
                        .HasColumnName("OrderStatus");

                    b.HasKey("Id");

                    b.ToTable("orders", "ordering");
                });

            modelBuilder.Entity("Ordering.Core.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasMaxLength(250)
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<int>("MasterId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedDate");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("orderItems", "ordering");
                });

            modelBuilder.Entity("Ordering.Core.Entities.OrderItem", b =>
                {
                    b.HasOne("Ordering.Core.Entities.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Core.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
