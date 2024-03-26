namespace ProductsApi.Tests.Models;

public class Product
{
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