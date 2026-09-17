namespace OnlineStoreAPI.DTOs;
public class ReviewDto
{
    public int Id{get; set;}
    public int UserId{get; set;}
    public string UserName{get; set;}=string.Empty;
    public int ProductId{get; set;}
    public string ProductTitle{get; set;}=string.Empty;
    public int Rating{get; set;}
    public string Comment{get; set;}=string.Empty;
    public DateTime CreatedAt{get;set;}
}