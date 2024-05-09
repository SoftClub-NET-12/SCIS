using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.SaleDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SaleService;

public class SaleService(DataContext context,IMapper mapper):ISaleService
{
    public async Task<Response<List<GetSaleDto>>> GetSalesAsync()
    {
        try
        {
            var sales = await context.Sales.ToListAsync();
            var mapped = mapper.Map<List<GetSaleDto>>(sales);
            return new Response<List<GetSaleDto>>(mapped);
        }
        catch (DbException DBe)
        {
            return new Response<List<GetSaleDto>>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<List<GetSaleDto>>(HttpStatusCode.InternalServerError, e.Message);

        }
    }

    public async Task<Response<GetSaleDto>> GetSaleByIdAsync(int id)
    {
        try
        {
            var existing = await context.Sales.FirstOrDefaultAsync(x => x.SaleId == id);
            if (existing == null) return new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Sale not found");
            var sales = mapper.Map<GetSaleDto>(existing);
            return new Response<GetSaleDto>(sales);
        }
        catch (DbException DBe)
        {
            return new Response<GetSaleDto>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<GetSaleDto>(HttpStatusCode.InternalServerError, e.Message);

        }
    }

    public async Task<Response<string>> AddSaleAsync(AddSaleDto add)
    {
        try
        {
            // var existing = await context.Sales.AnyAsync(e => e. == add.Name);
            // if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Sale already exists!");
            var mapped = mapper.Map<Sale>(add);
            await context.Sales.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (DbException DBe)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);

        }
    }

    public async Task<Response<string>> UpdateSaleAsync(UpdateSaleDto update)
    {
        try
        {
            var existing = await context.Sales.AnyAsync(e => e.SaleId == update.SaleId);
            if (!existing) return new Response<string>(HttpStatusCode.NotFound, "Sale Not Found!");
            var mapped = mapper.Map<Sale>(update);
            context.Sales.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.BadRequest, "Yet Update!");
        }
        catch (DbException DBe)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);

        }    }

    public async Task<Response<bool>> DeleteSaleAsync(int id)
    {
        try
        {
            var existing = await context.Sales.Where(e => e.SaleId == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found!");
            return new Response<bool>(true);
        }
        catch (DbException DBe)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);

        }    }
}