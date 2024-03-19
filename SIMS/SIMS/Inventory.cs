using SIMS.Exceptions;

namespace SIMS;

public class Inventory
{
    private List<Product> Products { get; } = new List<Product>();

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (Products.Any(p => p.Id == product.Id))
        {
            throw new ProductAlreadyExistsException();
        }

        Products.Add(product);
    }

    public void ViewAllProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }

        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }
    }

    public void UpdateProduct(int id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var existingProduct = FindProduct(id);

        if (existingProduct is null)
        {
            throw new ProductNotFoundException();
        }

        if (Products.Any(p => p.Id == product.Id) && product.Id != id)
        {
            throw new ProductAlreadyExistsException();
        }

        existingProduct.Name = product.Name ?? existingProduct.Name;
        existingProduct.Price = product.Price ?? existingProduct.Price;
        existingProduct.Quantity = product.Quantity ?? existingProduct.Quantity;
    }

    private Product? FindProduct(int id)
    {
        return Products.Find(p => p.Id == id);
    }

    public bool DeleteProduct(int id)
    {
        var product = FindProduct(id);

        if (product is null)
        {
            return false;
        }

        Products.Remove(product);
        return true;
    }

    public Product SearchProduct(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var product = FindProduct(id);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return product;
    }
}