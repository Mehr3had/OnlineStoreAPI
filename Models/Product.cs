namespace OnlineStoreAPI.Models;
public class Product
{
    public int Id{get; set;}
    public string Title{get; set;}=string.Empty;
    public string Description{get; set;}=string.Empty;
    public decimal Price{get; set;}=0;
    public int CategoryId{get; set;}
    public Category Category{get; set;}=null!;
    public ICollection<CartItem> CartItems{get; set;}=new List<CartItem>();
    public ICollection<OrderItem> OrderItems{get; set;}=new List<OrderItem>();
    public ICollection<ProductImage> Images{get; set;}=new List<ProductImage>();
    public ICollection<Review> Reviews{get; set;}=new List<Review>();
}