namespace Domain.DTOs.StockDto;

public class CreateStockDto
{
    public string? StockName { get; set; }
    public int LocationId { get; set; }
    public int UserId { get; set; }
}
