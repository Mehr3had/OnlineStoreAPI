using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface IPaymentService
{
    Task<PaymentDto> GetByOrderIdAsync(int orderId,int userId);
    Task<PaymentDto> PayAsync(int orderId,int userId);
}