namespace ProductsApi.Models;

public class ProductWithHistory : Product
{
    public ProductWithHistory(Product product)
    {
        Id = product.Id;
        ProductName = product.ProductName;
        Price = product.Price;
        Quantity = product.Quantity;
        IsAvailable = product.IsAvailable;
        History = new List<ProductHistory>();
    }

    public List<ProductHistory> History { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            Id = Id,
            ProductName = ProductName,
            Price = Price,
            Quantity = Quantity,
            IsAvailable = IsAvailable
        };
    }
}