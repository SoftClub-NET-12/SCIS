namespace Domain.Entities;

public class Supplier
{
 
    public int SupplierId { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? ContactInfo { get; set; }
    public int LocationId { get; set; }




    public List<ProductSupplier>? ProductSuppliers { get; set; }
    public Location? Location { get; set; }


}
