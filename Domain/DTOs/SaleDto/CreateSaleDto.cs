namespace Domain.DTOs.SaleDto;

public class CreateSaleDto
{
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime SaleDate { get; set; }
}
