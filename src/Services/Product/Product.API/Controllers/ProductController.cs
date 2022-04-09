using System.Net;
using Microsoft.AspNetCore.Mvc;
using Product.API.Repositories.Interfaces;

namespace Product.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("items")]
    [ProducesResponseType(typeof(IReadOnlyList<Entities.Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Entities.Product>>> ProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("items/{id:int}",Name="ProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Product>> ProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var product = await _productRepository.GetProductAsync(id);

        if (product == null)
        {
            _logger.LogError($"Product not found.");
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    [Route("items/withname/{name:minlength(1)}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Entities.Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Entities.Product>>> ProductsWithNameAsync(string name)
    {
        var products = await _productRepository.GetProductsWithNameAsync(name);
        if (products == null)
        {
            _logger.LogError($"Products not found.");
            return NotFound();
        }

        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.Product>> CreateProductAsync([FromBody] Entities.Product product)
    {
        await _productRepository.CreateProductAsync(product);

        return CreatedAtRoute("ProductById", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProductAsync([FromBody] Entities.Product product)
    {
        return Ok(await _productRepository.UpdateProductAsync(product));
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteProductByIdAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
        return NoContent();
    }

}