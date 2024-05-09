namespace Domain.Filters;

public class LocationFilter:PaginationFilter
{

    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
}
