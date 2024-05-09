namespace Domain.DTOs.StockProductDTO;

public class UpdateStockProductDto
{
    public int StockProductId { get; set; }
    public int Quantity { get; set; }



    public int StockId { get; set; }
    public int ProductId { get; set; }
}