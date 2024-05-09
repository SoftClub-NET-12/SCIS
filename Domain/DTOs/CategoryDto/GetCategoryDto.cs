namespace Domain.DTOs.CategoryDto;

public class GetCategoryDto
{
    public int CategoryId { get; set; } 
    public string Name { get; set; } = null!;
    public string Description { get; set; }=null!;
}
