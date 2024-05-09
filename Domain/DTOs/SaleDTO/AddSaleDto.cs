namespace Domain.DTOs.SaleDTO;

public class AddSaleDto
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public decimal TotalAmount { get; set; }

}