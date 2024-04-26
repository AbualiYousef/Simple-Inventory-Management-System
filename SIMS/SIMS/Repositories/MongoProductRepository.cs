using MongoDB.Bson;
using MongoDB.Driver;
using SIMS.Models;

namespace SIMS.Repositories;

public class MongoProductRepository: IProductRepository 
{
    private readonly IMongoCollection<Product> _products;

    public MongoProductRepository(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _products = database.GetCollection<Product>("Products");
    }

    public async Task AddProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        // Use a regular expression with the case-insensitive 'i' option.
        var filter = Builders<Product>.Filter.Regex("name", new BsonRegularExpression(name, "i"));
        return await _products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateProductAsync(ObjectId id, Product product)
    {
        var update = Builders<Product>.Update;
        var updateDefinition = new List<UpdateDefinition<Product>>();
        if (product.Name != null)
        {
            updateDefinition.Add(update.Set(p => p.Name, product.Name));
        }

        if (product.Price != null)
        {
            updateDefinition.Add(update.Set(p => p.Price, product.Price));
        }

        if (product.Quantity != null)
        {
            updateDefinition.Add(update.Set(p => p.Quantity, product.Quantity));
        }

        if (updateDefinition.Count == 0)
        {
            return false;
        }

        var combinedUpdate = update.Combine(updateDefinition);
        var result = await _products.UpdateOneAsync(p => p.Id == id, combinedUpdate);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(ObjectId id)
    {
        var result = await _products.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ProductExistsAsync(ObjectId id)
    {
        var count = await _products.CountDocumentsAsync(p => p.Id == id);
        return count > 0;
    }
}