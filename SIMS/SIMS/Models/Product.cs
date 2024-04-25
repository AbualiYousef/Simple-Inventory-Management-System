namespace SIMS.Models;

public class Product(string? name, decimal? price, int? quantity)
{
    public int Id { get;}
    public string? Name { get; } = name;
    public decimal? Price { get;} = price;
    public int? Quantity { get;} = quantity;

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}