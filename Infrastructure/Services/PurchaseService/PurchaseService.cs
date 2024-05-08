using System.Data.Common;
using AutoMapper;
using Domain.DTOs.Purchase;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PurchaseServices;

public class PurchaseService : IPurchaseService
{
     private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PurchaseService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<PagedResponse<List<GetPurchaseDto>>> GetPurchasesAsync(PurchaseFilter filter)
    {
        try
        {
            var Purchases = _context.Purchases.AsQueryable();
            if (filter.TotalAmount!=null)
                Purchases = Purchases.Where(x => x.TotalAmount==filter.TotalAmount);

            var result = await Purchases.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.PurchaseId).ToListAsync();
            var total = await Purchases.CountAsync();

            var response = _mapper.Map<List<GetPurchaseDto>>(result);
            return new PagedResponse<List<GetPurchaseDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPurchaseDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetPurchaseDto>> GetPurchaseByIdAsync(int purchaseId)
    {
        try
        {
            var existing = await _context.Purchases.FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
            if (existing == null) return new Response<GetPurchaseDto>(System.Net.HttpStatusCode.BadRequest, "Purchase not found");
            var Purchase = _mapper.Map<GetPurchaseDto>(existing);
            return new Response<GetPurchaseDto>(Purchase);
        }
        catch (Exception e)
        {
            return new Response<GetPurchaseDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<string>> CreatePurchaseAsync(AddPurchaseDto purchase)
    {
        try
        {
            var newPurchase = _mapper.Map<Purchase>(purchase);
            await _context.Purchases.AddAsync(newPurchase);
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


    public async Task<Response<string>> UpdatePurchaseAsync(UpdatePurchaseDto purchase)
    {
        try
        {
            var existing = await _context.Purchases.AnyAsync(x => x.PurchaseId == purchase.PurchaseId);
            if (!existing) return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Purchase not found");
            var newPurchase = _mapper.Map<Purchase>(purchase);
            _context.Purchases.Update(newPurchase);
            await _context.SaveChangesAsync();
            return new Response<string>("Purchase successfully updated");
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


    public async Task<Response<bool>> RemovePurchaseAsync(int purchaseId)
    {
        try
        {
            var existing = await _context.Purchases.Where(x => x.PurchaseId == purchaseId).ExecuteDeleteAsync();
            if (existing == 0)
            {
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Purchase not found");
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
