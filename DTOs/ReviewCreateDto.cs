namespace OnlineStoreAPI.DTOs;
public class ReviewCreateDto
{
    public int UserId{get; set;}
    public int ProductId{get; set;}
    public int Rating{get; set;}
    public string Comment{get; set;}=string.Empty;
}