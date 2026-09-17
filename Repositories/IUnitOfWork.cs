using Microsoft.EntityFrameworkCore.Storage;
namespace OnlineStoreAPI.Repositories;
public interface IUnitOfWork
{
    IUserRepository Users{get;}
    IProductRepository Products{get;}
    ICategoryRepository Categories{get;}
    ICartRepository Carts{get;}
    IOrderRepository Orders{get;}
    IPaymentRepository Payments{get;}
    IReviewRepository Reviews{get;}
    IProductImageRepository Images{get;}
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
}