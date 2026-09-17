using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Exceptions;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.Services;
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }

    public async Task<CartDto> AddItemAsync(int userId, CartItemCreateDto dto)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        var product=await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        var existingItem=await _unitOfWork.Carts.GetItemAsync(cart.Id,dto.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity+=dto.Quantity;
        }
        else
        {
            var newItem=new CartItem
            {
                CartId=cart.Id,
                ProductId=dto.ProductId,
                Quantity=dto.Quantity
            };
            await _unitOfWork.Carts.AddItemAsync(newItem);
        }
        await _unitOfWork.SaveChangesAsync();
        return await GetByUserIdAsync(userId);
    }

    public async Task ClearItemsAsync(int userId)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        await _unitOfWork.Carts.ClearItemsAsync(cart);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CartDto> DeleteItemAsync(int userId, int productId)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        var item=await _unitOfWork.Carts.GetItemAsync(cart.Id,productId);
        if (item == null)
        {
            throw new NotFoundException("Cart item not found.");
        }
        await _unitOfWork.Carts.DeleteItemAsync(item);
        await _unitOfWork.SaveChangesAsync();
        return await GetByUserIdAsync(userId);
    }

    public async Task<CartDto> GetByUserIdAsync(int userId)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        var items=cart.Items.Select(item=>new CartItemDto
        {
            ProductId=item.ProductId,
            Title=item.Product.Title,
            Quantity=item.Quantity,
            UnitPrice=item.Product.Price,
            Total=item.Product.Price*item.Quantity
        }).ToList();
        return new CartDto
        {
            UserId=cart.UserId,
            Items=items,
            GrandTotal=items.Sum(item=>item.Total)
        };
    }

    public async Task<CartDto> UpdateItemAsync(int userId, int productId, CartItemUpdateDto dto)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        var item=await _unitOfWork.Carts.GetItemAsync(cart.Id,productId);
        if (item == null)
        {
            throw new NotFoundException("Cart item not found.");
        }
        item.Quantity=dto.Quantity;
        await _unitOfWork.Carts.UpdateItemAsync(item);
        await _unitOfWork.SaveChangesAsync();
        return await GetByUserIdAsync(userId);
    }
}