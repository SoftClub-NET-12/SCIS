using System.Net;
using AutoMapper;
using Domain.DTOs.ProductDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddProductAsync(AddProductDto addProductDto)
    {
        try
        {
            var existing = await _context.Products.AnyAsync(e => e.Name == addProductDto.Name);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Product already exists!");
            var mapped = _mapper.Map<Product>(addProductDto);
            await _context.Products.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetProductDto>>> GetProductAsync(ProductFilter filter)
    {
        try
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                products = products.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            var product = await products.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await products.CountAsync();

            var response = _mapper.Map<List<GetProductDto>>(products);
            return new PagedResponse<List<GetProductDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetProductDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var existing = await _context.Products.Where(e => e.ProductId == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Product not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        try
        {
            var existing = await _context.Products.AnyAsync(e => e.ProductId == updateProductDto.ProductId);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Product not found!");
            
            var mapped = _mapper.Map<Product>(updateProductDto);
            _context.Products.Update(mapped);
            await _context.SaveChangesAsync();
            
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetProductDto>> GetProductByIdAsync(int id)
    {
        try
        {
            var existing = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (existing == null) return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not found");
            var product = _mapper.Map<GetProductDto>(existing);
            return new Response<GetProductDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetProductDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
