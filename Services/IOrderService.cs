using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface IOrderService
{
    Task<OrderDto> GetByIdAsync(int id,int userId);
    Task<List<OrderDto>> GetByUserIdAsync(int userId);
    Task<OrderDto> CheckoutAsync(int userId,int addressId);
}