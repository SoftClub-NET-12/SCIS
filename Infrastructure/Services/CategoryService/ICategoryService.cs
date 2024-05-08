using Domain.DTOs.CategoryDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    Task<PagedResponse<List<GetCategoriesDto>>> GetCategoriesAsync(CategoryFilter filter);
    Task<Response<string>> AddCategoryAsync(AddCategoryDto addCategotyDto);
    Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task<Response<bool>> DeleteCategoryAsync(int id);
}
