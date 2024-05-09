using Domain.Entities;

namespace Domain.Filters;

public class StockFilter:PaginationFilter
{
    public string? StockName { get; set; }
    public virtual Location? Location { get; set; }
}
