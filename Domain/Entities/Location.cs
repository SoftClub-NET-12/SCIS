namespace Domain.Entities;

public class Location
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;

  
    public List<Supplier>? Suppliers { get; set; }
    public List<Stock>? Stocks { get; set; }

}
