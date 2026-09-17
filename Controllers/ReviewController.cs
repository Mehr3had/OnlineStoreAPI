using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.Services;
using FluentValidation;
using OnlineStoreAPI.Validators;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IValidator<ReviewCreateDto> _validator;
    public ReviewController(IReviewService reviewService,IValidator<ReviewCreateDto> validator)
    {
        _reviewService=reviewService;
        _validator=validator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        var review=await _reviewService.GetByIdAsync(id);
        return Ok(review);
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetByProductId(int productId)
    {
        var reviews=await _reviewService.GetByProductIdAsync(productId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create(ReviewCreateDto dto)
    {
        var validationResult=await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var review=await _reviewService.CreateAsync(dto);
        return Ok(review);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.DeleteAsync(id);
        return NoContent();
    }
}