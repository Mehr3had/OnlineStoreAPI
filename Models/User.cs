namespace OnlineStoreAPI.Models;
public class User
{
    public int Id{get; set;}
    public string FirstName{get; set;}=string.Empty;
    public string LastName{get; set;}=string.Empty;
    public string Email{get; set;}=string.Empty;
    public string Password{get; set;}=string.Empty;
    public int RoleId{get; set;}
    public Role Role{get; set;}=null!;
    public Cart Cart{get; set;}=null!;
    public ICollection<Order> Orders{get; set;}=new List<Order>();
    public ICollection<Address> Addresses{get; set;}=new List<Address>();
    public ICollection<Review> Reviews{get; set;}=new List<Review>();
}