using Domain.DTOs.StockDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.StockService;

public interface IStockService
{
    public Task<PagedResponse<List<GetStockDto>>> GetStocksAsync(StockFilter filter);
    public Task<Response<GetStockDto>> GetStockByIdAsync(int id);
    public Task<Response<string>> AddStockAsync(AddStockDto add);
    public Task<Response<string>> UpdateStockAsync(UpdateStockDto update);
    public Task<Response<bool>> DeleteStockAsync(int id);
}