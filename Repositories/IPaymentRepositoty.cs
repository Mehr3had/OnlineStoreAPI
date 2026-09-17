using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;
public interface IPaymentRepository
{
    Task<Payment?> GetByOrderIdAsync(int orderId);
    Task AddAsync(Payment payment);
}