using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService=categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories=await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category=await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpGet("{id}/products")]
    public async Task<ActionResult<CategoryDetailsDto>> GetCategoryProducts(int id)
    {
        var category=await _categoryService.GetDetailsByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto dto)
    {
        var category=await _categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetCategory),new{id=category.Id},category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id,CategoryUpdateDto dto)
    {
        var category=await _categoryService.UpdateAsync(id,dto);
        return Ok(category);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<CategoryDto>> PatchCategory(int id,CategoryPatchDto dto)
    {
        var category=await _categoryService.PatchAsync(id,dto);
        return Ok(category);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}