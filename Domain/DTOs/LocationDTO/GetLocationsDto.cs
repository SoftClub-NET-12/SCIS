namespace Domain.DTOs.LocationDTO;

public class GetLocationsDto
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
}
