// code: fatih.unal date: 2025-04-21T14:01:00
using Microsoft.EntityFrameworkCore;
using FthAdmin.Infrastructure.Identity;

namespace FthAdmin.Infrastructure.Repositories
{
    public class RoleRepository
    {
        private readonly DbContext _context;
        public RoleRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationRole?> GetByIdAsync(int id)
        {
            return await _context.Set<ApplicationRole>().FindAsync(id);
        }
    }
}
