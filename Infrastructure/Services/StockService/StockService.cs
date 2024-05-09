using System.Data.Common;
using AutoMapper;
using Domain.DTOs.StockDTO;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using System.Linq;
using System.Net;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StockService;

public class StockService(DataContext context,IMapper mapper):IStockService
{
    public async Task<PagedResponse<List<GetStockDto>>> GetStocksAsync(StockFilter filter)
    {
        try
        {
            var stocks = context.Stocks.AsQueryable();
            if (!string.IsNullOrEmpty(filter.StockName))
                stocks = stocks.Where(e => e.StockName.ToLower().Contains(filter.StockName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Location.LocationName))
                stocks = stocks.Where(e => e.Location.LocationName.ToLower().Contains(filter.Location.LocationName.ToLower()));
            var stock = await stocks.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await stocks.CountAsync();
            var mapped = mapper.Map<List<GetStockDto>>(stocks);
            return new PagedResponse<List<GetStockDto>>(mapped, total, filter.PageNumber, filter.PageSize);
        }
        catch (DbException DBe)
        {
            return new PagedResponse<List<GetStockDto>>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetStockDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStockDto>> GetStockByIdAsync(int id)
    {
        try
        {
            var existing = await context.Stocks.FirstOrDefaultAsync(e => e.StockId == id);
            if (existing == null) return new Response<GetStockDto>(HttpStatusCode.NotFound, "Stock Not Found!");
            var mapped = mapper.Map<GetStockDto>(existing);
            return new Response<GetStockDto>(mapped);

        }
        catch (DbException DBe)
        {
            return new Response<GetStockDto>(HttpStatusCode.InternalServerError, DBe.Message);
        }
        catch (Exception e)
        {
            return new Response<GetStockDto>(HttpStatusCode.InternalServerError, e.Message);

        }
    }

    public async Task<Response<string>> AddStockAsync(AddStockDto add)
    {
        try
        {
            var existing = await context.Stocks.AnyAsync(e => e.StockName == add.StockName);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Stock Already Exisit");
            var mapped = mapper.Map<Stock>(add);
            await context.Stocks.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Added Success");
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

    public async Task<Response<string>> UpdateStockAsync(UpdateStockDto update)
    {
        try
        {
            var existing = await context.Stocks.AnyAsync(e => e.StockId == update.StockId);
            if (!existing) return new Response<string>(HttpStatusCode.NotFound, "Stock Not Found!");
            var mapped = mapper.Map<Stock>(update);
            context.Stocks.Update(mapped);
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

        }
    }

    public async Task<Response<bool>> DeleteStockAsync(int id)
    {
        try
        {
            var existing = await context.Stocks.Where(e => e.StockId == id).ExecuteDeleteAsync();
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

        }
    }
}