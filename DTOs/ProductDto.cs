namespace OnlineStoreAPI.DTOs;
public class ProductDto
{
    public int Id{get; set;}
    public string Title{get; set;}=string.Empty;
    public string Description{get; set;}=string.Empty;
    public decimal Price{get; set;}
    public int CategoryId{get; set;}
    public ProductCategoryDto? Category{get; set;}
    public List<ProductImageDto> Images{get; set;}=new();
}