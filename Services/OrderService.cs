using System.Transactions;
using Microsoft.AspNetCore.Http.Features;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Exceptions;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<OrderDto> CheckoutAsync(int userId,int addressId)
    {
        var cart=await _unitOfWork.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found.");
        }
        if (!cart.Items.Any())
        {
            throw new BadRequestException("Cart is empty.");
        }
        var user=await _unitOfWork.Users.GetByIdWithAddressesAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        var address=user.Addresses.FirstOrDefault(a=>a.Id==addressId);
        if (address == null)
        {
            throw new NotFoundException("Address not found.");
        }
        await using var transaction=await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order=new Order
            {
                UserId=userId,
                OrderDate=DateTime.Now,
                ShippingCity=address.City,
                ShippingStreet=address.Street,
                ShippingPostalCode=address.PostalCode
            };
            foreach(var cartItem in cart.Items)
            {
                var orderItem=new OrderItem
                {
                    ProductId=cartItem.ProductId,
                    Quantity=cartItem.Quantity,
                    UnitPrice=cartItem.Product.Price
                };
                order.Items.Add(orderItem);
            }
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.Carts.ClearItemsAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return await GetByIdAsync(order.Id,order.UserId);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<OrderDto> GetByIdAsync(int id,int userId)
    {
        var order=await _unitOfWork.Orders.GetByIdAsync(id);
        if (order == null || order.UserId!=userId)
        {
            throw new NotFoundException("Order not found.");
        }
        return MapToDto(order);
    }

    public async Task<List<OrderDto>> GetByUserIdAsync(int userId)
    {
        var orders=await _unitOfWork.Orders.GetByUserIdAsync(userId);
        return orders.Select(MapToDto).ToList();
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id=order.Id,
            UserId=order.UserId,
            OrderDate=order.OrderDate,
            ShippingCity=order.ShippingCity,
            ShippingStreet=order.ShippingStreet,
            ShippingPostalCode=order.ShippingPostalCode,
            Items=order.Items.Select(item=>new OrderItemDto
            {
                ProductId=item.ProductId,
                Title=item.Product.Title,
                Quantity=item.Quantity,
                UnitPrice=item.UnitPrice,
                Total=item.Quantity*item.UnitPrice
            }).ToList(),
            GrandTotal=order.Items.Sum(item=>item.Quantity*item.UnitPrice)
        };
    }
}