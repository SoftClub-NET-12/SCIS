using Domain.DTOs.StockProductDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.StockProductService
{
    public interface IStockProductService
    {
        Task<PagedResponse<List<GetStockProductDto>>> GetStockProductsAsync(StockProductFilter filter);
        Task<Response<GetStockProductDto>> GetStockProductByIdAsync(int stockProductId);
        Task<Response<string>> CreateStockProductAsync(AddStockProductDto stockProduct);
        Task<Response<string>> UpdateStockProductAsync(UpdateStockProductDto stockProduct);
        Task<Response<bool>> RemoveStockProductAsync(int stockProductId);
    }
}