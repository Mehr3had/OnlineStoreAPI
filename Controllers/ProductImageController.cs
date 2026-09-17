using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly IProductImageService _imageService;
    public ProductImageController(IProductImageService imageService)
    {
        _imageService=imageService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductImageDto>> GetById(int id)
    {
        var image=await _imageService.GetByIdAsync(id);
        return Ok(image);
    }
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ProductImageDto>>> GetByProductId(int productId)
    {
        var images=await _imageService.GetByProductIdAsync(productId);
        return Ok(images);
    }
    [HttpPost]
    public async Task<ActionResult<ProductImageDto>> Create(ProductImageCreateDto dto)
    {
        var image=await _imageService.CreateAsync(dto);
        return Ok(image);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _imageService.DeleteAsync(id);
        return NoContent();
    }
}