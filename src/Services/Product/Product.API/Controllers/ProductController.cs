using Microsoft.AspNetCore.Mvc;
using Product.API.Repositories;
using System.Net;


namespace Product.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IAsyncRepository<Entities.Product> _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IAsyncRepository<Entities.Product> productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Entities.Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Entities.Product>>> ProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id:int}",Name="ProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Product>> ProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogError($"Product not found.");
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.Product>> CreateProductAsync([FromBody] Entities.Product product)
    {
        await _productRepository.AddAsync(product);

        return CreatedAtRoute("ProductById", new { id = product.Id }, product);
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        await _productRepository.DeleteAsync(product);
        return NoContent();
    }

}