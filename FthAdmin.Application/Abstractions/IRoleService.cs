// code: fatih.unal date: 2025-04-22
using System.Collections.Generic;
using System.Threading.Tasks;
using FthAdmin.Application.Features.Roles.Queries;

namespace FthAdmin.Application.Abstractions
{
    public interface IRoleService
    {
        Task<string> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<RoleDto>> GetRolesAsync();
    }
}
