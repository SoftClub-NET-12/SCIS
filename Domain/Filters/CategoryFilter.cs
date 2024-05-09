namespace Domain.Filters;


public class CategoryFilter:PaginationFilter
{
    public string Name { get; set; } = null!;
    public string Description { get; set; }=null!;
}
