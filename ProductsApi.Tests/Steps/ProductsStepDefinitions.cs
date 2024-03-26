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

    [When("Product is added")]
    public async Task WhenProductIsAdded()
    {
        _product = await _productsClient.PostProduct(_testProduct);
    }

    [Then("Product should match correct product")]
    public void ThenProductShouldMatchCorrectProduct()
    {
        _product.ProductName.Should().Be(_testProduct.ProductName);
        _product.Price.Should().Be(_testProduct.Price);
        _product.Quantity.Should().Be(_testProduct.Quantity);
    }
}