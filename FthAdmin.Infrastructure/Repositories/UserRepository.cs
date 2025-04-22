// code: fatih.unal date: 2025-04-21T14:01:00
using Microsoft.EntityFrameworkCore;
using FthAdmin.Infrastructure.Identity;

namespace FthAdmin.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _context.Set<ApplicationUser>().FindAsync(id);
        }
    }
}
