namespace OnlineStoreAPI.DTOs;
public class PaymentDto
{
    public int Id{get; set;}
    public int OrderId{get; set;}
    public decimal Amount{get; set;}
    public DateTime PaymentDate{get; set;}
    public string Status{get; set;}=string.Empty;
}