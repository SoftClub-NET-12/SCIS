namespace Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
   


    public Category? Category { get; set; }
    public ICollection<ProductSupplier>? ProductSuppliers { get; set; }
    public ICollection<StockProduct>? StockProducts { get; set; }
    public List<Purchase>? Purchases { get; set; }
    public List<PriceHistory>? PriceHistories { get; set; }
    public List<Sale>? Sales { get; set; }

}
