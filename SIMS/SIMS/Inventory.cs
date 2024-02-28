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
} // End of Inventory class