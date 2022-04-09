namespace Product.API.Repositories.Interfaces;

public interface IProductRepository
{
       Task<IReadOnlyList<Entities.Product>> GetProductsAsync();
        Task<Entities.Product> GetProductAsync(int id);
        Task<IReadOnlyList<Entities.Product>> GetProductsWithNameAsync(string name);

        Task CreateProductAsync(Entities.Product product);
        Task<bool> UpdateProductAsync(Entities.Product product);
        Task<bool> DeleteProductAsync(int id);
}