namespace Domain.DTOs.ProductDto;

public class GetProductDto
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
