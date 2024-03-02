namespace SIMS;

public class Product
{
    // Properties
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    
    // Constructor
    public Product(string? name, decimal? price, int? quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }// End of Constructor
} // End of Product class