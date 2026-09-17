namespace OnlineStoreAPI.Models;
public class CartItem
{
    public int Id{get; set;}
    public int ProductId{get; set;}
    public Product Product{get; set;}=null!;
    public int Quantity{get; set;}=1;
    public int CartId{get; set;}
    public Cart Cart{get; set;}=null!;
}