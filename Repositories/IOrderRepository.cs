using OnlineStoreAPI.Models;
namespace OnlineStoreAPI.Repositories;
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int Id);
    Task<List<Order>> GetByUserIdAsync(int userId);
    Task AddAsync(Order order);
}