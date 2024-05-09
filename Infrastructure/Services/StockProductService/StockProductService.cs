using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.StockProductDTO; // Updated namespace
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StockProductService // Updated namespace
{
    public class StockProductService : IStockProductService // Updated class name
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public StockProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }





        public async Task<PagedResponse<List<GetStockProductDto>>> GetStockProductsAsync(StockProductFilter filter) // Updated method signature
        {
            try
            {
                var stockProducts = _context.StockProducts.AsQueryable(); // Updated entity reference
                if (filter.Quantity>0) // Updated property reference
                    stockProducts = stockProducts.Where(x => x.Quantity==filter.Quantity); // Updated property reference
                var result = await stockProducts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();
                var total = await stockProducts.CountAsync();

                var response = _mapper.Map<List<GetStockProductDto>>(result); // Updated DTO reference
                return new PagedResponse<List<GetStockProductDto>>(response, total, filter.PageNumber, filter.PageSize); // Updated DTO reference
            }
            catch (Exception e)
            {
                return new PagedResponse<List<GetStockProductDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetStockProductDto>> GetStockProductByIdAsync(int stockProductId) // Updated method signature
        {
            try
            {
                var existing = await _context.StockProducts.FirstOrDefaultAsync(x => x.StockProductId == stockProductId); // Updated entity reference
                if (existing == null) return new Response<GetStockProductDto>(HttpStatusCode.BadRequest, "StockProduct not found"); // Updated entity reference
                var stockProduct = _mapper.Map<GetStockProductDto>(existing); // Updated DTO reference
                return new Response<GetStockProductDto>(stockProduct); // Updated DTO reference
            }
            catch (Exception e)
            {
                return new Response<GetStockProductDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        
        
        

        public async Task<Response<string>> CreateStockProductAsync(AddStockProductDto stockProduct) // Updated method signature
        {
            try
            {
                var newStockProduct = _mapper.Map<StockProduct>(stockProduct); // Updated entity reference
                await _context.StockProducts.AddAsync(newStockProduct); // Updated entity reference
                await _context.SaveChangesAsync();
                return new Response<string>("Successfully created ");
            }
            catch (DbException e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateStockProductAsync(UpdateStockProductDto stockProduct) // Updated method signature
        {
            try
            {
                var existing = await _context.StockProducts.AnyAsync(x => x.StockProductId == stockProduct.StockProductId); // Updated entity reference
                if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "StockProduct not found"); // Updated entity reference
                var newStockProduct = _mapper.Map<StockProduct>(stockProduct); // Updated entity reference
                _context.StockProducts.Update(newStockProduct); // Updated entity reference
                await _context.SaveChangesAsync();
                return new Response<string>("StockProduct successfully updated"); // Updated entity reference
            }
            catch (DbException e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> RemoveStockProductAsync(int stockProductId) // Updated method signature
        {
            try
            {
                var existing = await _context.StockProducts.Where(x => x.StockProductId == stockProductId).ExecuteDeleteAsync(); // Updated entity reference
                return existing == 0
                    ? new Response<bool>(HttpStatusCode.BadRequest, "StockProduct not found") // Updated entity reference
                    : new Response<bool>(true);
            }
            catch (DbException e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
