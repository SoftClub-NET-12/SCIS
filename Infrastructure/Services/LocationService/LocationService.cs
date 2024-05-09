using System.Net;
using AutoMapper;
using Domain.DTOs.LocationDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.LocationService;

public class LocationService(DataContext context, IMapper mapper):ILocationService
{
    public async Task<Response<string>> AddLocationAsync(AddLocationDto addLocationDto)
    {
        try
        {
            var existing = await context.Locations.AnyAsync(e => e.LocationName == addLocationDto.LocationName);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Location already exists!");
            var mapped = mapper.Map<Location>(addLocationDto);
            await context.Locations.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteLocationAsync(int id)
    {
        try
        {
            var existing = await context.Locations.Where(e => e.LocationId == id).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Location not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetLocationsDto>>> GetLocationsAsync(LocationFilter filter)
    {
        try
        {
            var locations = context.Locations.AsQueryable();
            if ( !string.IsNullOrEmpty(filter.LocationName))
            locations = locations.Where(x => x.LocationName.ToLower().Contains(filter.LocationName.ToLower()));
            if ( !string.IsNullOrEmpty(filter.Address))
            locations = locations.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            var result = await locations.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await locations.CountAsync();

            var response = mapper.Map<List<GetLocationsDto>>(result);
            return new PagedResponse<List<GetLocationsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetLocationsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateLocationAsync(UpdateLocationDto updateLocationDto)
    {
        try
        {
            var existing = await context.Locations.AnyAsync(e => e.LocationId == updateLocationDto.LocationId);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Location not found!");
            var mapped = mapper.Map<Location>(updateLocationDto);
            context.Locations.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
