namespace Domain.Entities;

public class StockProduct
{
    public int StockProductId { get; set; }
    public int Quantity { get; set; }



    public int StockId { get; set; }
    public int ProductId { get; set; }

    

    public Stock? Stock { get; set; }
    public Product? Product { get; set; }
   
    
}
