namespace Domain.DTOs.ProductDTO;

public class AddProductDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
   
}
