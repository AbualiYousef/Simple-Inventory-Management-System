using SIMS.Models;

namespace SIMS.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> GetProductByNameAsync(string name);
    Task<bool> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
}