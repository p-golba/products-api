using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductsApi.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<ProductHistory>? ProductHistory { get; set; }
}

public class ProductHistory
{
    public DateTime Timestamp { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}