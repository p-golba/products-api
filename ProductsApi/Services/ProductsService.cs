using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class ProductsService
{
    private readonly IMongoCollection<ProductWithHistory> _productsCollection;

    public ProductsService(IOptions<ProductsDatabaseSettings> productsDatabaseSettings)
    {
        var mongoClient = new MongoClient(productsDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productsDatabaseSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<ProductWithHistory>(
            productsDatabaseSettings.Value.ProductsCollectionName
        );
    }

    public async Task<List<Product>> GetAsync() =>
        (await _productsCollection.Find(_ => true).ToListAsync()).ConvertAll(product => (Product) product);

    public async Task<ProductWithHistory?> GetAsync(string id) =>
        await _productsCollection.Find(product => product.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ProductWithHistory newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updatedProduct)
    {
        var oldProduct = await GetAsync(id);
        updatedProduct.Id = id;
        if (updatedProduct.Quantity == 0)
        {
            updatedProduct.IsAvailable = false;
        }
        var newProduct = new ProductWithHistory(updatedProduct)
        {
            History = oldProduct.History
        };
        newProduct.History!.Add(new ProductHistory(oldProduct.ToProduct()));
        await _productsCollection.ReplaceOneAsync(x => x.Id == id, newProduct);
    }


    public async Task RemoveAsync(string id) =>
        await _productsCollection.DeleteOneAsync(product => product.Id == id);
}