using Product.API.Repositories.Interfaces;
using System.Collections.Generic;

namespace Product.API.Repositories;

public class ProductRepository:IProductRepository
{
    public Task<IReadOnlyList<Entities.Product>> GetProductsAsync()
    {
        var products = new List<Entities.Product>()
        {
            new Entities.Product
            {
                Id = 1,
                Name = "mug",
                Description = "cool mug",
                Price = 50
            }
        };
        return Task.FromResult<IReadOnlyList<Entities.Product>>(products);
    }

    public Task<Entities.Product> GetProductAsync(int id)
    {
        var product = 
            new Entities.Product
            {
                Id = 1,
                Name = "mug",
                Description = "cool mug",
                Price = 50
            };
        return Task.FromResult<Entities.Product>(product);
    }

    public Task<IReadOnlyList<Entities.Product>> GetProductsWithNameAsync(string name)
    {
        var products = new List<Entities.Product>()
        {
            new Entities.Product
            {
                Id = 1,
                Name = "mug",
                Description = "cool mug",
                Price = 50
            }
        };
        return Task.FromResult<IReadOnlyList<Entities.Product>>(products);
    }

    public Task CreateProductAsync(Entities.Product product)
    {
        return Task.FromResult(string.Empty);
    }

    public Task<bool> UpdateProductAsync(Entities.Product product)
    {
        return Task.FromResult(true);
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        return Task.FromResult(true);
    }
}