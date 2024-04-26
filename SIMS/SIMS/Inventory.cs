using MongoDB.Bson;
using SIMS.Exceptions;
using SIMS.Models;
using SIMS.Repositories;

namespace SIMS;

public class Inventory(string connectionString, string databaseName)
{
    private readonly MongoProductRepository _mongoProductRepository = new(connectionString, databaseName);

    public async Task AddProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        await _mongoProductRepository.AddProductAsync(product);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _mongoProductRepository.GetAllProductsAsync();
    }

    public async Task UpdateProductAsync(ObjectId id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (!await _mongoProductRepository.ProductExistsAsync(id))
        {
            throw new ProductNotFoundException();
        }

        await _mongoProductRepository.UpdateProductAsync(id, product);
    }

    public async Task<bool> DeleteProductAsync(ObjectId id)
    {
        if (!await _mongoProductRepository.ProductExistsAsync(id))
        {
            throw new ProductNotFoundException();
        }

        return await _mongoProductRepository.DeleteProductAsync(id);
    }

    public async Task<Product> SearchProductByNameAsync(string name)
    {
        var product = await _mongoProductRepository.GetProductByNameAsync(name);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return product;
    }
}