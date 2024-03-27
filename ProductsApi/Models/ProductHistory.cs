namespace ProductsApi.Models;

public class ProductHistory
{
    public ProductHistory(Product product)
    {
        Timestamp = DateTime.Now;
        Product = product;
    }
    public DateTime Timestamp { get; set; }
    public Product Product { get; set; }
}