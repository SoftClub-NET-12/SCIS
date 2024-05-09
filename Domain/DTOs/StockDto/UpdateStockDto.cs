namespace Domain.DTOs.StockDto;

public class UpdateStockDto
{
    public int StockId { get; set; }
    public string? StockName { get; set; }
    public int LocationId { get; set; }   
    public int UserId { get; set; }
    
}
