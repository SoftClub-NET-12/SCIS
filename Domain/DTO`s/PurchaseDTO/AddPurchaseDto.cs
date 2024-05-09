namespace Domain.DTOs.Purchase;

public class AddPurchaseDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }

}
