using Domain.DTOProductSuplierDTO;
using Domain.DTOs.ProductSuplierDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services;

public interface IProductSuplierService
{
     Task<PagedResponse<List<GetProductSuplierDto>>> GetProductSupliersAsync(ProductSuplierFilter filter);
    Task<Response<GetProductSuplierDto>> GetProductSuplierByIdAsync(int productSuplierId);
    Task<Response<string>> CreateProductSuplierAsync(AddProductSupplierDto productSuplier);
    Task<Response<string>> UpdateProductSuplierAsync(UpdateProductSuplierDto ProductSuplier);
    Task<Response<bool>> RemoveProductSuplierAsync(int productSuplierId);
}
