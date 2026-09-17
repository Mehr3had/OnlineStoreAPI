using Microsoft.EntityFrameworkCore.Storage;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;
namespace OnlineStoreAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context; 
    public IUserRepository Users{get;}

    public IProductRepository Products{get;}

    public ICategoryRepository Categories{get;}

    public ICartRepository Carts{get;}

    public IOrderRepository Orders{get;}

    public IPaymentRepository Payments{get;}

    public IReviewRepository Reviews{get;}

    public IProductImageRepository Images{get;}

    public UnitOfWork(ApplicationDbContext context,IUserRepository userRepository,IProductRepository productRepository
            ,ICategoryRepository categoryRepository,ICartRepository cartRepository,IOrderRepository orderRepository
            ,IPaymentRepository paymentRepository,IReviewRepository reviewRepository,IProductImageRepository productImageRepository)
    {
        _context=context;
        Users=userRepository;
        Products=productRepository;
        Categories=categoryRepository;
        Carts=cartRepository;
        Orders=orderRepository;
        Payments=paymentRepository;
        Reviews=reviewRepository;
        Images=productImageRepository;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}