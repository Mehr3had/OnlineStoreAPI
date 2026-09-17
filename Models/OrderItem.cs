namespace OnlineStoreAPI.Models;
public class OrderItem
{
    public int Id{get; set;}
    public int ProductId{get; set;}
    public Product Product{get; set;}=null!;
    public int Quantity{get; set;}=1;
    public decimal UnitPrice{get; set;}=0;
    public int OrderId{get; set;}
    public Order Order{get; set;}=null!;
}