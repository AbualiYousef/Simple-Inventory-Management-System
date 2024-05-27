using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SIMS.Models;

public class Product(string? name, decimal? price, int? quantity)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; private set; } = name;

    [BsonElement("price")]
    public decimal? Price { get; private set; } = price;

    [BsonElement("quantity")]
    public int? Quantity { get; private set; } = quantity;

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}