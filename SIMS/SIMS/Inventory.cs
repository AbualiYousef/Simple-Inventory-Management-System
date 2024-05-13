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
        var existingProduct = await _sqlProductRepository.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException();
        }

        var updatedProduct = new Product(
            product.Name ?? existingProduct.Name,
            product.Price ?? existingProduct.Price,
            product.Quantity ?? existingProduct.Quantity
        );

        var updateResult = await _sqlProductRepository.UpdateProductAsync(id, updatedProduct);
        if (!updateResult)
        {
            throw new Exception("Failed to update the product.");
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var productExists = await _sqlProductRepository.GetProductByIdAsync(id);
        if (productExists == null)
        {
            throw new ProductNotFoundException();
        }

        var deleteResult = await _sqlProductRepository.DeleteProductAsync(id);
        if (!deleteResult)
        {
            throw new Exception("Failed to delete the product.");
        }
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