namespace Domain.DTOs.LocationDto;

public class GetLocationDto
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
}
