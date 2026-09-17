namespace OnlineStoreAPI.DTOs;
public class OrderDto
{
    public int Id{get; set;}
    public int UserId{get; set;}
    public DateTime OrderDate{get; set;}
    public List<OrderItemDto> Items{get; set;}=new();
    public decimal GrandTotal{get; set;}
    public string ShippingCity{get; set;}=string.Empty;
    public string ShippingStreet{get; set;}=string.Empty;
    public string ShippingPostalCode{get; set;}=string.Empty;
}