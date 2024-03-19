namespace SIMS;

public class Product(int id, string? name, decimal? price, int? quantity)
{
    public int Id { get; set; } = id;
    public string? Name { get; set; } = name;
    public decimal? Price { get; set; } = price;
    public int? Quantity { get; set; } = quantity;

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}