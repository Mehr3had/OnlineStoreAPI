namespace OnlineStoreAPI.DTOs;
public class CartItemDto
{
    public int ProductId{get; set;}
    public string Title{get; set;}=string.Empty;
    public int Quantity{get; set;}=1;
    public decimal UnitPrice{get; set;}
    public decimal Total{get; set;}
}