using System.Net;
using AutoMapper;
using Domain.DTOs.CategoryDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CategoryService;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<string>> AddCategoryAsync(AddCategoryDto addCategotyDto)
    {
        try
        {
            var existing = await context.Categories.AnyAsync(e => e.Name == addCategotyDto.Name);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Category already exists!");
            var mapped = mapper.Map<Category>(addCategotyDto);
            await context.Categories.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteCategoryAsync(int id)
    {
        try
        {
            var existing = await context.Categories.Where(e => e.CategoryId == id).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Category not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetCategoriesDto>>> GetCategoriesAsync(CategoryFilter filter)
    {
        try
        {
            var categories = context.Categories.AsQueryable();
            if ( !string.IsNullOrEmpty(filter.Name))
            categories = categories.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            var result = await categories.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await categories.CountAsync();

            var response = mapper.Map<List<GetCategoriesDto>>(result);
            return new PagedResponse<List<GetCategoriesDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCategoriesDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var existing = await context.Categories.AnyAsync(e => e.CategoryId == updateCategoryDto.CategoryId);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = mapper.Map<Category>(updateCategoryDto);
            context.Categories.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
