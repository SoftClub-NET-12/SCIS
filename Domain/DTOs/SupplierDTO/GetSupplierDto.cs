namespace Domain.DTOs.SupplierDTO;

public class GetSupplierDto
{
    public int SupplierId { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? ContactInfo { get; set; }
    public int LocationId { get; set; }
}