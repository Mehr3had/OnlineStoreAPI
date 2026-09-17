using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
    public OrderController(IOrderService orderService)
    {
        _orderService=orderService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var userId=GetUserId();
        var order=await _orderService.GetByIdAsync(id,userId);
        return Ok(order);
    }

    [HttpGet("me")]
    public async Task<ActionResult<List<OrderDto>>> GetByUserId()
    {
        var userId=GetUserId();
        var orders=await _orderService.GetByUserIdAsync(userId);
        return Ok(orders);
    }

    [HttpPost("me/checkout")]
    public async Task<ActionResult<OrderDto>> Checkout(int addressId)
    {
        var userId=GetUserId();
        var order=await _orderService.CheckoutAsync(userId,addressId);
        return Ok(order);
    }
}