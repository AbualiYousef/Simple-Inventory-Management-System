using SIMS.Exceptions;

namespace SIMS;

public class Inventory
{
    private List<Product> Products { get; } = [];

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (Products.Any(p => p.Name == product.Name))
        {
            throw new ProductAlreadyExistsException();
        }

        Products.Add(product);
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

    public void UpdateProduct(string currentProductName, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var existingProduct = FindProduct(currentProductName);

        if (existingProduct is null)
        {
            throw new ProductNotFoundException();
        }

        if (Products.Any(p => p.Name == product.Name) && product.Name != currentProductName)
        {
            throw new ProductAlreadyExistsException();
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
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

    public Product SearchProduct(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        var product = FindProduct(name);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return product;
    }

} // End of Inventory class