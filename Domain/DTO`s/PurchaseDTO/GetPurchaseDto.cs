namespace Domain.DTOs.Purchase;

public class GetPurchaseDto
{
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }


}
