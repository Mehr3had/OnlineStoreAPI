namespace OnlineStoreAPI.DTOs;
public class ProductUpdateDto
{
    public string Title{get; set;}=string.Empty;
    public decimal Price{get; set;}
    public int CategoryId{get; set;}
}