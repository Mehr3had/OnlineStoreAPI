using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;
public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByIdWithAddressesAsync(int id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}