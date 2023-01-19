using Common;
using Models;
using Microsoft.AspNetCore.Mvc;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductPriceRepository _priceRepository;

    public ProductController(IProductRepository productRepository, IProductPriceRepository priceRepository)
    {
        _productRepository = productRepository;
        _priceRepository = priceRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productRepository.GetAll());
    }

    [AllowAnonymous]
    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetById(int? productId)
    {
        if (!productId.HasValue || productId.Value == 0)
        {
            return BadRequest(new ErrorModelDTO()
            {
                ErrorMessage = "Invalid ID",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        var product = await _productRepository.GetById(productId.Value);

        if (product == null)
        {
            return NotFound(new ErrorModelDTO()
            {
                ErrorMessage = "Invalid ID",
                StatusCode = StatusCodes.Status404NotFound
            });
        }

        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDTO objDto)
    {
        var current = await _productRepository.GetById(id);

        if (string.IsNullOrEmpty(current.Name))
            return NotFound();

        current.Name = objDto.Name;

        return Ok(await _productRepository.Update(current));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDTO objDto)
    {
        return Ok(await _productRepository.Create(objDto));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var current = await _productRepository.GetById(id);

        if (string.IsNullOrEmpty(current.Name))
            return NotFound();

        return Ok(await _productRepository.Delete(id) > 0);
    }

    [AllowAnonymous]
    [HttpGet("/{productId:int}/prices")]
    public async Task<IActionResult> GetAllPrices([FromRoute] int productId)
    {
        var product = await _productRepository.GetById(productId);
        if (string.IsNullOrEmpty(product.Name))
        {
            return NotFound();
        }
        return Ok(await _priceRepository.GetAll(productId));
    }

    [AllowAnonymous]
    [HttpGet("/{productId:int}/prices/{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int productId, int id)
    {
        var product = await _productRepository.GetById(productId);
        var price = await _priceRepository.GetById(id);

        if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(price.Size))
        {
            return NotFound();
        }
        return Ok(await _priceRepository.GetById(id));
    }

    [HttpPost("/{productId:int}/prices")]
    public async Task<IActionResult> Create([FromBody] ProductPriceDTO objDto)
    {
        return Ok(await _priceRepository.Create(objDto));
    }

    [HttpPut("/{productId:int}/prices/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int productId, int id, [FromBody] ProductPriceDTO objDto)
    {
        var product = await _productRepository.GetById(productId);
        var price = await _priceRepository.GetById(id);

        if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(price.Size))
            return NotFound();

        price.Size = objDto.Size;
        price.Price = objDto.Price;
        price.ProductId = objDto.ProductId;

        return Ok(await _priceRepository.Update(price));
    }

    [HttpDelete("/{productId:int}/prices/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int productId, int id)
    {
        var product = await _productRepository.GetById(productId);
        var price = await _priceRepository.GetById(id);

        if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(price.Size))
            return NotFound();

        return Ok(await _productRepository.Delete(id) > 0);
    }
}
