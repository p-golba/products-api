using ProductsApi.Tests.Models;
using Refit;

namespace ProductsApi.Tests;

public interface IProductsClient
{
    [Get("/Products/{id}")]
    Task<Product> GetProduct(string id);
    
    [Post("/Products")]
    Task<Product> PostProduct(Product newProduct);

    [Put("/Products/{id}")]
    Task<ApiResponse<Product>> PutProduct(string id, [Body] Product updatedProduct);
}