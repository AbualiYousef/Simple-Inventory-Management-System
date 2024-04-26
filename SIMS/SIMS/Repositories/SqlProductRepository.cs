using System.Data;
using Microsoft.Data.SqlClient;
using SIMS.Models;

namespace SIMS.Repositories;

public class SqlProductRepository(string connectionString)
{
    public async Task AddProductAsync(Product product)
    {
        const string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
        await ExecuteNonQueryAsync(query, new Dictionary<string, object>
        {
            { "@Name", product.Name! },
            { "@Price", product.Price! },
            { "@Quantity", product.Quantity! }
        });
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        const string query = "SELECT Id, Name, Price, Quantity FROM Products";
        var products = new List<Product>();
        await using var reader = await ExecuteReaderAsync(query);
        while (await reader.ReadAsync())
        {
            var product = ReadProduct(reader);
            products.Add(product);
        }

        return products;
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        const string query = "SELECT Id, Name, Price, Quantity FROM Products WHERE Name = @Name";
        await using var reader = await ExecuteReaderAsync(query, new Dictionary<string, object> { { "@Name", name } });
        if (!await reader.ReadAsync()) return null;
        return ReadProduct(reader);
    }

    public async Task<bool> UpdateProductAsync(int id, Product product)
    {
        const string query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";
        var parameters = new Dictionary<string, object>
        {
            { "@Id", id },
            { "@Name", product.Name! },
            { "@Price", product.Price! },
            { "@Quantity", product.Quantity! }
        };
        return await ExecuteNonQueryAsync(query, parameters) > 0;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        const string query = "DELETE FROM Products WHERE Id = @Id";
        return await ExecuteNonQueryAsync(query, new Dictionary<string, object> { { "@Id", id } }) > 0;
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        const string query = "SELECT COUNT(*) FROM Products WHERE Id = @Id";
        return (int)await ExecuteScalarAsync(query, new Dictionary<string, object> { { "@Id", id } }) > 0;
    }

    private async Task<int> ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters)
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(query, connection);
        AddParameters(command, parameters);
        try
        {
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            throw;
        }
    }

    private async Task<SqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object>? parameters = null)
    {
        var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(query, connection);
        if (parameters != null)
        {
            AddParameters(command, parameters);
        }

        try
        {
            await connection.OpenAsync();
            return await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            throw;
        }
    }

    private async Task<object> ExecuteScalarAsync(string query, Dictionary<string, object> parameters)
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(query, connection);
        AddParameters(command, parameters);
        try
        {
            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result ?? 0;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            throw;
        }
    }

    private static void AddParameters(SqlCommand command, Dictionary<string, object> parameters)
    {
        foreach (var (key, value) in parameters)
        {
            command.Parameters.AddWithValue(key, value ?? DBNull.Value);
        }
    }

    private static Product ReadProduct(IDataRecord reader)
    {
        var id = reader.GetInt32(reader.GetOrdinal("Id"));
        var name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
        var price = reader.IsDBNull(reader.GetOrdinal("Price"))
            ? null
            : (decimal?)reader.GetDecimal(reader.GetOrdinal("Price"));
        var quantity = reader.IsDBNull(reader.GetOrdinal("Quantity"))
            ? null
            : (int?)reader.GetInt32(reader.GetOrdinal("Quantity"));

        var product = new Product(name, price, quantity)
        {
            Id = id
        };

        return product;
    }
}