using SIMS.Exceptions;
using SIMS.Models;
using SIMS.Repositories;

namespace SIMS;

public class Inventory(string connectionString)
{
    private readonly SqlProductRepository _sqlProductRepository = new(connectionString);

    public async Task AddProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        await _sqlProductRepository.AddProductAsync(product);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _sqlProductRepository.GetAllProductsAsync();
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (!await _sqlProductRepository.ProductExistsAsync(id))
        {
            throw new ProductNotFoundException();
        }

        await _sqlProductRepository.UpdateProductAsync(id, product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        if (!await _sqlProductRepository.ProductExistsAsync(id))
        {
            throw new ProductNotFoundException();
        }

        return await _sqlProductRepository.DeleteProductAsync(id);
    }

    public async Task<Product> SearchProductByNameAsync(string name)
    {
        var product = await _sqlProductRepository.GetProductByNameAsync(name);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return product;
    }
}