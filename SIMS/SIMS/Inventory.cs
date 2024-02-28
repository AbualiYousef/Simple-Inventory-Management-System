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
} // End of Inventory class