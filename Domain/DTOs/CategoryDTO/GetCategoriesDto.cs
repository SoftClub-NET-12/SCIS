namespace Domain.DTOs.CategoryDTO;

public class GetCategoriesDto
{
    public int CategoryId { get; set; } 
    public string Name { get; set; } = null!;
    public string Description { get; set; }=null!;
}
