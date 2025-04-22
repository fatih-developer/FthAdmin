// code: fatih.unal date: 2025-04-22
using System.Collections.Generic;
using System.Threading.Tasks;
using FthAdmin.Application.Features.Users.Queries;

namespace FthAdmin.Application.Abstractions
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(string userName, string email, string password);
        Task<bool> AssignRoleAsync(string userId, string roleName);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<UserDto>> GetUsersAsync();
    }
}
