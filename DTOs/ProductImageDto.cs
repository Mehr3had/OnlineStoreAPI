namespace OnlineStoreAPI.DTOs;
public class ProductImageDto
{
    public int Id{get; set;}
    public int ProductId{get; set;}
    public string ProductTitle{get; set;}=string.Empty;
    public string ImageUrl{get; set;}=string.Empty;
}