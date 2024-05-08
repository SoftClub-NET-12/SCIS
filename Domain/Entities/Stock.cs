namespace Domain.Entities;

public class Stock
{


    public int StockId { get; set; }
    public string? StockName { get; set; }
    public int LocationId { get; set; }   
    public int UserId { get; set; }



    public User? User { get; set; }
    public ICollection<StockProduct>? StockProducts  { get; set; }
    public Location? Location { get; set; }
   
}
