namespace SIMS;

public class Inventory
{
    private List<Product> Products { get; } = [];

    public bool AddProduct(Product? product)
    {
        if (product is null)
        {
            return false;
        }

        if (Products.Any(p => p.Name == product.Name))
        {
            return false;
        }

        Products.Add(product);
        return true;
    }

    public void ViewAllProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products found");
        }

        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }
    }

    public bool UpdateProduct(string currentProductName, string? newProductName,
        decimal? newProductPrice, int? newProductQuantity)
    {
        var product = FindProduct(currentProductName);

        if (product is null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(newProductName))
        {
            product.Name = newProductName;
        }

        if (newProductPrice.HasValue)
        {
            product.Price = newProductPrice.Value;
        }

        if (newProductQuantity.HasValue)
        {
            product.Quantity = newProductQuantity.Value;
        }

        return true;
    }

    private Product? FindProduct(string name)
    {
        return Products.Find(p => p.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool DeleteProduct(string name)
    {
        var product = FindProduct(name);

        if (product is null)
        {
            return false;
        }

        Products.Remove(product);
        return true;
    }

    public void SearchProduct(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name cannot be null or empty");
            return;
        }

        var product = FindProduct(name);

        if (product is null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine(product);
    }
}// End of Inventory class