using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentController:ControllerBase
{
    private readonly IPaymentService _paymentService;
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService=paymentService;
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult<PaymentDto>> GetByOrderId(int orderId)
    {
        var userId=GetUserId();
        var payment=await _paymentService.GetByOrderIdAsync(orderId,userId);
        return Ok(payment);
    }
    
    [HttpPost("order/{orderId}/pay")]
    public async Task<ActionResult<PaymentDto>> Pay(int orderId)
    {
        var userId=GetUserId();
        var payment=await _paymentService.PayAsync(orderId,userId);
        return Ok(payment);
    }
}