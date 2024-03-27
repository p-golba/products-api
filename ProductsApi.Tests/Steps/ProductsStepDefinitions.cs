using FluentAssertions;
using ProductsApi.Tests.Models;
using Refit;

namespace ProductsApi.Tests.Steps;

[Binding]
public sealed class ProductStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IProductsClient _productsClient;
    private Product _testProduct;
    private Product _product;

    public ProductStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _productsClient = RestService.For<IProductsClient>("http://localhost:5000/api");
    }

    [Given("Correct Product")]
    public void GivenCorrectProduct()
    {
        _testProduct = new Product()
        {
            ProductName = "Test Product",
            Price = 19.99m,
            Quantity = 10
        };
    }

    [Given("Product with negative price")]
    public void GivenProductWithNegativePrice()
    {
        _testProduct = new Product()
        {
            ProductName = "Test Product",
            Price = -20.00m,
            Quantity = 1
        };
    }

    [Given("Product with (.*) quantity")]
    public void GivenProductWithQuantity(int number)
    {
        _testProduct = new Product()
        {
            ProductName = "test",
            Price = 20m,
            Quantity = number
        };
    }
    
    
    [When("Product is added")]
    public async Task WhenProductIsAdded()
    {
        _product = await _productsClient.PostProduct(_testProduct);
    }

    [When("Product is updated")]
    public async Task WhenProductIsUpdated()
    {
        var updated = new Product()
        {
            ProductName = "Updated",
            Price = 1,
            Quantity = 12
        };
        await _productsClient.PutProduct(_product.Id!, updated);
    }

    [Then("Product should match correct product")]
    public void ThenProductShouldMatchCorrectProduct()
    {
        _product.ProductName.Should().Be(_testProduct.ProductName);
        _product.Price.Should().Be(_testProduct.Price);
        _product.Quantity.Should().Be(_testProduct.Quantity);
    }

    [Then("Product should not be created")]
    public void ThenProductShouldNotBeCreated()
    {
        _product.Should().Be(null);
    }

    [Then("Product should not be available")]
    public void ThenProductShouldNotBeAvailable()
    {
        _product.Status.Should().Be(false);
    }

    [Then("Product should contain changes in history")]
    public async Task ThenProductShouldContainChangesInHistory()
    {
        var result = await _productsClient.GetProduct(_product.Id!);
        result.History![0].Product!.ProductName.Should().Be(_product.ProductName);
        result.History![0].Product!.Price.Should().Be(_product.Price);
        result.History![0].Product!.Quantity.Should().Be(_product.Quantity);
        result.ProductName.Should().NotBe(_testProduct.ProductName);
        result.Price.Should().NotBe(_testProduct.Price);
        result.Quantity.Should().NotBe(_testProduct.Quantity);
    }
    
}