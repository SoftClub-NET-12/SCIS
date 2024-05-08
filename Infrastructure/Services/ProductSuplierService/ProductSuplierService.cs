using System.Data.Common;
using AutoMapper;
using Domain.DTOProductSuplierDTO;
using Domain.DTOs.ProductSuplierDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductSuplierService;

public class ProductSuplierService: IProductSuplierService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductSuplierService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<PagedResponse<List<GetProductSuplierDto>>> GetProductSupliersAsync(ProductSuplierFilter filter)
    {
        try
        {
            var ProductSupliers = _context.ProductSuppliers.AsQueryable();
            if (filter.SupplierId!=null)
                ProductSupliers = ProductSupliers.Where(x => x.SupplierId ==filter.SupplierId);
            var result = await ProductSupliers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.SupplierId).ToListAsync();
            var total = await ProductSupliers.CountAsync();

            var response = _mapper.Map<List<GetProductSuplierDto>>(result);
            return new PagedResponse<List<GetProductSuplierDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetProductSuplierDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetProductSuplierDto>> GetProductSuplierByIdAsync(int productSuplierId)
    {
        try
        {
            var existing = await _context.ProductSuppliers.FirstOrDefaultAsync(x => x.SupplierId == productSuplierId);
            if (existing == null) return new Response<GetProductSuplierDto>(System.Net.HttpStatusCode.BadRequest, "ProductSuplier not found");
            var ProductSuplier = _mapper.Map<GetProductSuplierDto>(existing);
            return new Response<GetProductSuplierDto>(ProductSuplier);
        }
        catch (Exception e)
        {
            return new Response<GetProductSuplierDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<string>> CreateProductSuplierAsync(AddProductSupplierDto productSuplier)
    {
        try
        {
            var existing = await _context.ProductSuppliers.AnyAsync(x => x.SupplierId == productSuplier.SupplierId);
            if (existing) return new Response<string>(System.Net.HttpStatusCode.BadRequest, "productSuplier already exists");
            var newProductSuplier = _mapper.Map<ProductSupplier>(productSuplier);
            await _context.ProductSuppliers.AddAsync(newProductSuplier);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (DbException e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> UpdateProductSuplierAsync(UpdateProductSuplierDto productSuplier)
    {
        try
        {
            var existing = await _context.ProductSuppliers.AnyAsync(x => x.ProductSupplierId == productSuplier.ProductSupplierId);
            if (!existing) return new Response<string>(System.Net.HttpStatusCode.BadRequest, "ProductSuplier not found");
            var newProductSuplier = _mapper.Map<ProductSupplier>(productSuplier);
            _context.ProductSuppliers.Update(newProductSuplier);
            await _context.SaveChangesAsync();
            return new Response<string>("ProductSuplier successfully updated");
        }
        catch (DbException e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<bool>> RemoveProductSuplierAsync(int productSuplierId)
    {
        try
        {
            var existing = await _context.ProductSuppliers.Where(x => x.ProductSupplierId == productSuplierId).ExecuteDeleteAsync();
            if (existing == 0)
            {
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "ProductSuplier not found");
            }
            return new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
