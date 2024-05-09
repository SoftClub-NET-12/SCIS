using Domain.DTOs.ProductDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<PagedResponse<List<GetProductDto>>> GetProductAsync(ProductFilter filter);
    Task<Response<string>> AddProductAsync(AddProductDto addProductDto);
    Task<Response<string>> UpdateProductAsync(UpdateProductDto updateProductDto);
    Task<Response<bool>> DeleteProductAsync(int id);
    Task<Response<GetProductDto>> GetProductByIdAsync(int id);
}
