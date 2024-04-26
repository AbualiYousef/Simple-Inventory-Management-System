namespace SIMS.Models;

public class Product(string? name, decimal? price, int? quantity)
{
    public int Id { get; set; }
    public string? Name { get; private set; } = name;
    public decimal? Price { get; private set; } = price;
    public int? Quantity { get; private set; } = quantity;

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}