// code: fatih.unal date: 2025-04-22
using System.Collections.Generic;
using System.Threading.Tasks;
using FthAdmin.Application.Abstractions;
using FthAdmin.Application.Features.Roles.Queries;
using FthAdmin.Infrastructure.Contexts;
using FthAdmin.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FthAdmin.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppIdentityDbContext _context;
        public RoleService(RoleManager<ApplicationRole> roleManager, AppIdentityDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<string> CreateRoleAsync(string roleName)
        {
            var role = new ApplicationRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new System.Exception(string.Join("; ", result.Errors));
            return role.Id.ToString();
        }
        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) throw new System.Exception("Rol bulunamadı");
            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            var result = new List<RoleDto>();
            foreach (var role in roles)
            {
                result.Add(new RoleDto
                {
                    Id = role.Id.ToString(),
                    Name = role.Name
                });
            }
            return result;
        }
    }
}
