using Domain.DTOs.LocationDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.LocationService;

public interface ILocationService
{
    Task<PagedResponse<List<GetLocationsDto>>> GetLocationsAsync(LocationFilter filter);
    Task<Response<string>> AddLocationAsync(AddLocationDto addLocationDto);
    Task<Response<string>> UpdateLocationAsync(UpdateLocationDto updateLocationDto);
    Task<Response<bool>> DeleteLocationAsync(int id);
}
