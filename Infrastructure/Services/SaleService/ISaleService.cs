using Domain.DTOs.SaleDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.SaleService;

public interface ISaleService
{
    public Task<Response<List<GetSaleDto>>> GetSalesAsync();
    public Task<Response<GetSaleDto>> GetSaleByIdAsync(int id);
    public Task<Response<string>> AddSaleAsync(AddSaleDto add);
    public Task<Response<string>> UpdateSaleAsync(UpdateSaleDto update);
    public Task<Response<bool>> DeleteSaleAsync(int id);
}