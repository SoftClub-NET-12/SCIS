using Domain.DTOs.Purchase;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PurchaseServices;

public interface IPurchaseService
{
    
    Task<PagedResponse<List<GetPurchaseDto>>> GetPurchasesAsync(PurchaseFilter filter);
    Task<Response<GetPurchaseDto>> GetPurchaseByIdAsync(int purchaseId);
    Task<Response<string>> CreatePurchaseAsync(AddPurchaseDto purchase);
    Task<Response<string>> UpdatePurchaseAsync(UpdatePurchaseDto purchase);
    Task<Response<bool>> RemovePurchaseAsync(int purchaseId);
}
