
using Domain.DTOs.UserDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<PagedResponse<List<GetUserDto>>> GetUsersAsync(UserFilter filter);

    Task<Response<GetUserDto>> GetUserByIdAsync(int userId);



    Task<Response<string>> CreateUserAsync(CreateUserDto user);
    Task<Response<string>> UpdateUserAsync(UpdateUserDto user);
    Task<Response<bool>> RemoveUserAsync(int UserId);
    // Task<Response<bool>> ValidatePasswordAsync(string name,string password);
    Task<bool> ValidatePasswordAsync(string name,string password);
    
}
