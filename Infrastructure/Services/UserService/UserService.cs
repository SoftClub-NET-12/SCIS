using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.UserDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserService;

public class UserService(DataContext _context, IMapper _mapper) : IUserService
{


    public async Task<PagedResponse<List<GetUserDto>>> GetUsersAsync(UserFilter filter)
    {
        try
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.UserName))
                users = users.Where(x => x.UserName!.ToLower().Contains(filter.UserName.ToLower()));
            var result = await users.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .OrderBy(x => x.UserId)
                .ToListAsync();
            var total = await users.CountAsync();

            var response = _mapper.Map<List<GetUserDto>>(result);
            return new PagedResponse<List<GetUserDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUserDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetUserDto>> GetUserByIdAsync(int userId)
    {
        try
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (existing == null) return new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not found");
            var user = _mapper.Map<GetUserDto>(existing);
            return new Response<GetUserDto>(user);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateUserAsync(CreateUserDto user)
    {
        try
        {
            var existing = await _context.Users.AnyAsync(x => x.UserName == user.UserName);
           // user.Password=Guid.NewGuid().ToString();
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "User already exists");
            var newUser = _mapper.Map<User>(user);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created user ");
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


    public async Task<Response<string>> UpdateUserAsync(UpdateUserDto updateUser)
    {
        try
        {
            var existing = await _context.Users.AnyAsync(x => x.UserId == updateUser.UserId);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "User not found");
            var newUser = _mapper.Map<User>(updateUser);
            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
            return new Response<string>("User successfully updated");
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


    public async Task<Response<bool>> RemoveUserAsync(int userId)
    {
        try
        {
            var existing = await _context.Users.Where(x => x.UserId == userId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "User not found")
                : new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }

    }

    //  public async Task<Response<bool>> ValidatePasswordAsync(string name,string password)
    // {
    //     try
    //     {
    //         var existing = await _context.Customers.AnyAsync(x => x.Name == name && x.Address==password);
    //         return existing == false
    //             ? new Response<bool>(HttpStatusCode.BadRequest, "Customer not found")
    //             : new Response<bool>(true);
    //     }
    //     catch (DbException e)
    //     {
    //         return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
    //     }

    // }

    public async Task<bool> ValidatePasswordAsync(string userName, string password)
    {

        var existing = await _context.Users.AnyAsync(x => x.UserName == userName && x.Password == password);
        return existing == false
            ? false
            : true;
    }

}

