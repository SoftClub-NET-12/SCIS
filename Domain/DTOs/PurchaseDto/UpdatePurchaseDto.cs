namespace Domain.DTOs.PurchaseDto;

public class UpdatePurchaseDto
{
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PurshaseDate { get; set; }
}
