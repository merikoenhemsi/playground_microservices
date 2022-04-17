using Microsoft.EntityFrameworkCore;

namespace Product.API.Data;

public class ProductContextSeed
{
    public static async Task SeedAsync(ProductContext ProductContext)
    {
        if (!await ProductContext.Products.AnyAsync())
        {
            await ProductContext.Products.AddRangeAsync(GetPreconfiguredProducts());
            await ProductContext.SaveChangesAsync();
        }
    }

    private static IEnumerable<Entities.Product> GetPreconfiguredProducts()
    {
        return new List<Entities.Product>()
        {
            new ()
            {
                Name = "ProductName",
                Description = "ProductDesc",
                Price = 100
            }

        };
    }
}