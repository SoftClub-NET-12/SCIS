using Domain.DTOs.PriceHistoryDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PriceHistoryService;

public interface IPriceHistoryService
{
    Task<PagedResponse<List<GetPriceHistoryDto>>> GetPriceHistoryAsync(PriceHistoryFilter filter);
    Task<Response<string>> AddPriceHistoryAsync(AddPriceHistoryDto addPriceHistoryDto);
    Task<Response<string>> UpdatePriceHistoryAsync(UpdatePriceHistoryDto updatePriceHistoryDto);
    Task<Response<bool>> DeletePriceHistoryAsync(int id);
    Task<Response<GetPriceHistoryDto>> GetPriceHistoryByIdAsync(int id);
}
