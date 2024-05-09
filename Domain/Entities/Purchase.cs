namespace Domain.Entities;

public class Purchase
{
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }   
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PurshaseDate { get; set; }


    public Product? Product { get; set; }

}

