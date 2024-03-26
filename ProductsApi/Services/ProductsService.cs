using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class ProductsService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductsService(IOptions<ProductsDatabaseSettings> productsDatabaseSettings)
    {
        var mongoClient = new MongoClient(productsDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productsDatabaseSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<Product>(
            productsDatabaseSettings.Value.ProductsCollectionName
        );
    }

    public async Task<List<Product>> GetAsync() =>
        await _productsCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) =>
        await _productsCollection.Find(product => product.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updatedProduct) =>
        await _productsCollection.ReplaceOneAsync(product => product.Id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await _productsCollection.DeleteOneAsync(product => product.Id == id);
}