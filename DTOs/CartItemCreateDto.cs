namespace OnlineStoreAPI.DTOs;
public class CartItemCreateDto
{
    public int ProductId{get; set;}
    public int Quantity{get; set;}=1;
}