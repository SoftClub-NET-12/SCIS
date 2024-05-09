using System.Net;
using AutoMapper;
using Domain.DTOs.PriceHistoryDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PriceHistoryService;

public class PriceHistoryService : IPriceHistoryService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PriceHistoryService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddPriceHistoryAsync(AddPriceHistoryDto addPriceHistoryDto)
    {
        try
        {   
            var mapped = _mapper.Map<PriceHistory>(addPriceHistoryDto);
            await _context.PriceHistories.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

     public async Task<PagedResponse<List<GetPriceHistoryDto>>> GetPriceHistoryAsync(PriceHistoryFilter filter)
    {
        try
        {
            var priceHistories = _context.PriceHistories.AsQueryable();
            
            var product = await priceHistories.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await priceHistories.CountAsync();

            var response = _mapper.Map<List<GetPriceHistoryDto>>(priceHistories);
            return new PagedResponse<List<GetPriceHistoryDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPriceHistoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeletePriceHistoryAsync(int id)
    {
        try
        {
            var existing = await _context.PriceHistories.Where(e => e.PriceHistoryId == id).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"PriceHistory not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdatePriceHistoryAsync(UpdatePriceHistoryDto updatePriceHistoryDto)
    {
        try
        {
            var existing = await _context.PriceHistories.AnyAsync(e => e.PriceHistoryId == updatePriceHistoryDto.PriceHistoryId);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "PriceHistory not found!");
            var mapped = _mapper.Map<PriceHistory>(updatePriceHistoryDto);
            _context.PriceHistories.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

     public async Task<Response<GetPriceHistoryDto>> GetPriceHistoryByIdAsync(int id)
    {
        try
        {
            var existing = await _context.PriceHistories.FirstOrDefaultAsync(x => x.PriceHistoryId == id);
            if (existing == null) return new Response<GetPriceHistoryDto>(HttpStatusCode.BadRequest, "PriceHistory not found");
            var PriceHistory = _mapper.Map<GetPriceHistoryDto>(existing);
            return new Response<GetPriceHistoryDto>(PriceHistory);
        }
        catch (Exception e)
        {
            return new Response<GetPriceHistoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
