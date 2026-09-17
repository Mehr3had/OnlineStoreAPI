namespace OnlineStoreAPI.DTOs;
public class CartDto
{
    public int UserId{get; set;}
    public List<CartItemDto> Items{get; set;}=new();
    public decimal GrandTotal{get; set;} 
}