namespace Domain.DTOs.StockDTO;

public class AddStockDto
{
    
    public string? StockName { get; set; }
    public int LocationId { get; set; }   
    public int UserId { get; set; }
}