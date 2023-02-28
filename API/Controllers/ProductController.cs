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

    [HttpGet("{categoryId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(int categoryId)
    {
        return Ok(await _productRepository.GetAll(categoryId));
    }

    [AllowAnonymous]
    [HttpGet("{categoryId:int}/{productId:int}")]
    public async Task<IActionResult> GetById(int? categoryId, int? productId)
    {
        if (!categoryId.HasValue || categoryId.Value == 0 || !productId.HasValue || productId.Value == 0)
        {
            return BadRequest(new ErrorModelDTO()
            {
                ErrorMessage = "Invalid ID",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        var product = await _productRepository.GetById(categoryId.Value, productId.Value);

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

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Update(int productId, [FromBody] ProductDTO objDto)
    {
        var current = await _productRepository.GetById(objDto.CategoryId, productId);

        if (string.IsNullOrEmpty(current.Name))
            return NotFound();

        current.Name = objDto.Name;
        current.Number = objDto.Number;
        current.InMyCollection = objDto.InMyCollection;
        current.Image = objDto.Image;        

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
        return Ok(await _productRepository.Delete(id) > 0);
    }                
}
