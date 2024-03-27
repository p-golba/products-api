using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.Services;

namespace ProductsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductsService _productsService;

    public ProductsController(ProductsService productsService) => _productsService = productsService;

    [HttpGet]
    public async Task<List<Product>> Get() =>
        await _productsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ProductWithHistory>> Get(string id)
    {
        var product = await _productsService.GetAsync(id);
        if (product is null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        var product = new ProductWithHistory(newProduct);
        await _productsService.CreateAsync(product);

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updatedProduct)
    {
        await _productsService.UpdateAsync(id, updatedProduct);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productsService.RemoveAsync(id);
        return NoContent();
    }
}