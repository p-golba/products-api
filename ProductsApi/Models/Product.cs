using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductsApi.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string ProductName { get; set; } = null!;

    [Range(0, double.PositiveInfinity)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)] 
    public int Quantity { get; set; }
    public bool IsAvailable { get; set; }
}