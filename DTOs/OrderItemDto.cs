namespace OnlineStoreAPI.DTOs;
public class OrderItemDto
{
    public int ProductId{get; set;}
    public string Title{get; set;}=string.Empty;
    public int Quantity{get; set;}
    public decimal UnitPrice{get; set;}
    public decimal Total{get; set;}
}