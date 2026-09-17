namespace OnlineStoreAPI.Models;
public class Address
{
    public int Id{get; set;}
    public string City{get; set;}=string.Empty;
    public string Street{get; set;}=string.Empty;
    public string PostalCode{get; set;}=string.Empty;
    public int UserId{get; set;}
    public User User{get; set;}=null!;
}