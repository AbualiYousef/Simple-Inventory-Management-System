using Microsoft.Extensions.Configuration;
using SIMS.Exceptions;
using SIMS.Models;

namespace SIMS;

public class Program
{
    private static IConfiguration? Configuration { get; set; }

    public static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        if (connectionString == null)
        {
            throw new ConnectionStringNotFoundException();
        }

        var inventory = new Inventory(connectionString);

        while (true)
        {
            DisplayMenu();
            var choice = GetMenuChoice();
            await ProcessMenuChoice(choice, inventory);
            Console.WriteLine();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View All Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. Search Product");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    private static int GetMenuChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            Console.Write("Enter your choice: ");
        }

        return choice;
    }

    private static async Task ProcessMenuChoice(int choice, Inventory inventory)
    {
        switch (choice)
        {
            case 1:
                await AddProduct(inventory);
                break;
            case 2:
                await ViewAllProducts(inventory);
                break;
            case 3:
                await UpdateProduct(inventory);
                break;
            case 4:
                await DeleteProduct(inventory);
                break;
            case 5:
                await SearchProduct(inventory);
                break;
            case 6:
                Console.WriteLine("Exiting program...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }

    private static async Task AddProduct(Inventory inventory)
    {
        Console.Write("Enter product name: ");
        var name = ReadNonEmptyString();

        Console.Write("Enter product price: ");
        var price = ReadDecimal();

        Console.Write("Enter product quantity: ");
        var quantity = ReadInteger();

        try
        {
            await inventory.AddProductAsync(new Product(name, price, quantity));
            Console.WriteLine("Product added successfully.");
        }
        catch (ProductAlreadyExistsException)
        {
            Console.WriteLine("Failed to add product. Product already exists.");
        }
    }

    private static async Task ViewAllProducts(Inventory inventory)
    {
        var products = await inventory.GetAllProductsAsync();

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

    private static async Task UpdateProduct(Inventory inventory)
    {
        Console.Write("Enter product ID to update: ");
        var id = ReadInteger();

        Console.Write("Enter new product name (leave empty to keep current name): ");
        var newProductNameInput = Console.ReadLine()?.Trim();
        var newProductName = string.IsNullOrEmpty(newProductNameInput) ? null : newProductNameInput;

        Console.Write("Enter new product price (leave empty to keep current price): ");
        var newProductPriceInput = Console.ReadLine()?.Trim();
        decimal? newProductPrice =
            string.IsNullOrEmpty(newProductPriceInput) ? null : decimal.Parse(newProductPriceInput);

        Console.Write("Enter new product quantity (leave empty to keep current quantity): ");
        var newProductQuantityInput = Console.ReadLine()?.Trim();
        int? newProductQuantity =
            string.IsNullOrEmpty(newProductQuantityInput) ? null : int.Parse(newProductQuantityInput);

        try
        {
            await inventory.UpdateProductAsync(id, new Product(newProductName, newProductPrice, newProductQuantity));
            Console.WriteLine("Product updated successfully.");
        }
        catch (ProductNotFoundException)
        {
            Console.WriteLine("Failed to update product. Product not found.");
        }
    }

    private static async Task DeleteProduct(Inventory inventory)
    {
        Console.Write("Enter product ID to delete: ");
        var id = ReadInteger();
        try
        {
            var deleted = await inventory.DeleteProductAsync(id);
            Console.WriteLine(deleted
                ? "Product deleted successfully."
                : "Failed to delete product.");
        }
        catch (ProductNotFoundException)
        {
            Console.WriteLine("Failed to delete product. Product not found.");
        }
    }

    private static async Task SearchProduct(Inventory inventory)
    {
        Console.Write("Enter product name to search: ");
        var name = ReadNonEmptyString();
        try
        {
            var product = await inventory.SearchProductByNameAsync(name);
            Console.WriteLine(product);
        }
        catch (ProductNotFoundException)
        {
            Console.WriteLine("Failed to search product. Product not found.");
        }
    }


    private static string ReadNonEmptyString()
    {
        string input;
        do
        {
            input = Console.ReadLine()?.Trim()!;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please enter a non-empty string.");
            }
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    private static decimal ReadDecimal()
    {
        decimal result;
        while (!decimal.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input. Please enter a valid decimal number.");
        }

        return result;
    }

    private static int ReadInteger()
    {
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }

        return result;
    }
}