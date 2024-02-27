namespace SIMS;

public class Inventory
{
    // Products property
    private List<Product> Products { get; set; } = [];

    // AddProduct method
    public bool AddProduct(Product? product)
    {
        //check if the product is null
        if (product is null)
        {
            return false;
        }

        //check if the product already exists
        if (Products.Any(p => p.Name == product.Name))
        {
            return false;
        }

        //add the product to the list
        Products.Add(product);
        return true;
    } // End of AddProduct method

    //ViewAllProducts method
    public void ViewAllProducts()
    {
        //check if there are no products
        if (Products.Count == 0)
        {
            Console.WriteLine("No products found");
        }

        //print all the products
        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }
    } // End of ViewAllProducts method

    //UpdateProduct method
    public bool UpdateProduct(string currentProductName, string? newProductName,
        decimal? newProductPrice, int? newProductQuantity)
    {
        //find the product
        var product = FindProduct(currentProductName);

        //check if the product is null
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
    } // End of UpdateProduct method

    //FindProduct method
    private Product? FindProduct(string name)
    {
        //find the product
        return Products.Find(p => p.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));
    } // End of FindProduct method

    //DeleteProduct method
    public bool DeleteProduct(string name)
    {
        //find the product
        var product = FindProduct(name);

        //check if the product is null
        if (product is null)
        {
            return false;
        }

        //remove the product
        Products.Remove(product);
        return true;
    } // End of DeleteProduct method

    //SearchProduct method
    public void SearchProduct(string name)
    {
        //check if the name is null or empty
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name cannot be null or empty");
            return;
        }

        //find the product
        var product = FindProduct(name);

        //check if the product is null
        if (product is null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        //print the product
        Console.WriteLine(product);
    } // End of SearchProduct method
} // End of Inventory class