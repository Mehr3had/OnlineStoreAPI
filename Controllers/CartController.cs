using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;
namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
    public CartController(ICartService cartService)
    {
        _cartService=cartService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<CartDto>> GetMyCart()
    {
        var userId=GetUserId();
        var cart=await _cartService.GetByUserIdAsync(userId);
        return Ok(cart);
    }

    [HttpPost("me/items")]
    public async Task<ActionResult<CartDto>> AddItem(CartItemCreateDto dto)
    {
        var userId=GetUserId();
        var cart=await _cartService.AddItemAsync(userId,dto);
        return Ok(cart);
    }

    [HttpPut("me/items/{productId}")]
    public async Task<ActionResult<CartDto>> UpdateItem(int productId,CartItemUpdateDto dto)
    {
        var userId=GetUserId();
        var cart=await _cartService.UpdateItemAsync(userId,productId,dto);
        return Ok(cart);
    }

    [HttpDelete("me/items/{productId}")]
    public async Task<ActionResult<CartDto>> DeleteItem(int productId)
    {
        var userId=GetUserId();
        var cart=await _cartService.DeleteItemAsync(userId,productId);
        return Ok(cart);
    }
    
    [HttpDelete("me/items")]
    public async Task<IActionResult> ClearCart()
    {
        var userId=GetUserId();
        await _cartService.ClearItemsAsync(userId);
        return NoContent();
    }

}