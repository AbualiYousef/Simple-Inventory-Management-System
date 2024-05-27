using MongoDB.Bson;
using SIMS.Models;

namespace SIMS.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByNameAsync(string name);
    Task<bool> UpdateProductAsync(ObjectId id, Product product);
    Task<bool> DeleteProductAsync(ObjectId id);
}