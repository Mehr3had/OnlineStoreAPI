using OnlineStoreAPI.Models;
using OnlineStoreAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await Task.CompletedTask;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.Include(u=>u.Role).FirstOrDefaultAsync(u=>u.Email==email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u=>u.Id==id);
    }

    public async Task<User?> GetByIdWithAddressesAsync(int id)
    {
        return await _context.Users.Include(u=>u.Addresses).FirstOrDefaultAsync(u=>u.Id==id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await Task.CompletedTask;
    }
}