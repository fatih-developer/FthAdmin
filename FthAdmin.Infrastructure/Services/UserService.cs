// code: fatih.unal date: 2025-04-22
using System.Collections.Generic;
using System.Threading.Tasks;
using FthAdmin.Application.Abstractions;
using FthAdmin.Application.Features.Users.Queries;
using FthAdmin.Infrastructure.Contexts;
using FthAdmin.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FthAdmin.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppIdentityDbContext _context;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<string> CreateUserAsync(string userName, string email, string password)
        {
            var user = new ApplicationUser { UserName = userName, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new System.Exception(string.Join("; ", result.Errors));
            return user.Id.ToString();
        }
        public async Task<bool> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new System.Exception("Kullanıcı bulunamadı");
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) throw new System.Exception("Rol bulunamadı");
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new System.Exception("Kullanıcı bulunamadı");
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserDto
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = new List<string>(roles)
                });
            }
            return result;
        }
    }
}
