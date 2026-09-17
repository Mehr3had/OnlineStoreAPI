using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<ProductPatchDto> _validator;
    public ProductController(IProductService productService,IValidator<ProductPatchDto> validator)
    {
        _productService=productService;
        _validator=validator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products=await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product=await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto dto)
    {
        var product=await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetProduct),new{id=product.Id},product);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id,ProductUpdateDto dto)
    {
        var product=await _productService.UpdateAsync(id,dto);
        return Ok(product);
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> PatchProduct(int id,ProductPatchDto dto)
    {
        var validationResult=await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var product=await _productService.PatchAsync(id,dto);
        return Ok(product);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}