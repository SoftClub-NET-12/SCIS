namespace Domain.DTOs.StockProductDto;

public class CreateStockProductDto
{
    public int Quantity { get; set; }

    public int StockId { get; set; }
    public int ProductId { get; set; }
}
