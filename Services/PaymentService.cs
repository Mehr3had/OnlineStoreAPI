using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Exceptions;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<PaymentDto> GetByOrderIdAsync(int orderId,int userId)
    {
        var order=await _unitOfWork.Orders.GetByIdAsync(orderId);
        if(order==null || order.UserId != userId)
        {
            throw new NotFoundException("Order not found.");
        }
        var payment=await _unitOfWork.Payments.GetByOrderIdAsync(orderId);
        if (payment == null)
        {
            throw new NotFoundException("Payment not found.");
        }
        return MapToDto(payment);
    }

    public async Task<PaymentDto> PayAsync(int orderId,int userId)
    {
        var order=await _unitOfWork.Orders.GetByIdAsync(orderId);
        if (order == null || order.UserId!=userId)
        {
            throw new NotFoundException("Order not found.");
        }
        var existingPayment=await _unitOfWork.Payments.GetByOrderIdAsync(orderId);
        if (existingPayment != null)
        {
            return MapToDto(existingPayment);
        }
        var amount=order.Items.Sum(item=>item.UnitPrice*item.Quantity);
        var payment=new Payment
        {
            OrderId=order.Id,
            Amount=amount,
            PaymentDate=DateTime.Now,
            Status="Paid"
        };
        await _unitOfWork.Payments.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(payment);
    }

    private static PaymentDto MapToDto(Payment payment)
    {
        return new PaymentDto
        {
            Id=payment.Id,
            OrderId=payment.OrderId,
            Amount=payment.Amount,
            PaymentDate=payment.PaymentDate,
            Status=payment.Status
        };
    }
}