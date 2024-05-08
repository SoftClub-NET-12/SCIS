namespace Domain.Entities;

public class ProductSupplier
{
    public int ProductSupplierId { get; set; }
    public int ProductId { get; set; }
    public int SupplierId { get; set; }



    public Supplier? Supplier { get; set; }
    public Product? MyProperty { get; set; }
}
