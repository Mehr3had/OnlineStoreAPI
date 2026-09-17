namespace OnlineStoreAPI.Models;
public class Order
{
    public int Id{get; set;}
    public DateTime OrderDate{get; set;}=DateTime.Now;
    public int UserId{get; set;}
    public User User{get; set;}=null!;
    public ICollection<OrderItem> Items{get; set;}=new List<OrderItem>();
    public Payment Payment{get; set;}=null!;
    public string ShippingCity{get; set;}=string.Empty;
    public string ShippingStreet{get; set;}=string.Empty;
    public string ShippingPostalCode{get; set;}=string.Empty;
}