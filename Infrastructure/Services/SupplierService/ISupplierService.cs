using Domain.DTOs.SupplierDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<PagedResponse<List<GetSupplierDto>>> GetSuppliersAsync(SupplierFilter filter);
        Task<Response<GetSupplierDto>> GetSupplierByIdAsync(int supplierId);
        Task<Response<string>> CreateSupplierAsync(AddSupplierDto supplier);
        Task<Response<string>> UpdateSupplierAsync(UpdateSupplierDto supplier);
        Task<Response<bool>> RemoveSupplierAsync(int supplierId);
    }
}