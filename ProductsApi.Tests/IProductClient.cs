using ProductsApi.Tests.Models;
using Refit;

namespace ProductsApi.Tests;

public interface IProductsClient
{
    [Get("/Products")]
    Task<IEnumerable<Product>> GetProducts();

    [Post("/Products")]
    Task<Product> PostProduct(Product newProduct);
}