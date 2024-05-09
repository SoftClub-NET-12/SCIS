namespace Domain.Filters;

public class PurchaseFilter : PaginationFilter
{
    public decimal TotalAmount { get; set; }

}
