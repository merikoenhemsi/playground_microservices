using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using System.Reflection;

namespace Product.API.Data;

public class ProductContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<Entities.Product> Products { get; set; }
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}