// code: fatih.unal date: 2025-04-21T10:08:39
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FthAdmin.Domain.Entities;
using FthAdmin.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using FthAdmin.Persistence.Contexts;

namespace FthAdmin.Persistence.Repositories
{
    public class ServerRepository : IGenericRepository<Server, Guid>
    {
        private readonly FthAdminDbContext _context;
        public ServerRepository(FthAdminDbContext context)
        {
            _context = context;
        }
        public async Task<Server?> GetAsync(Expression<Func<Server, bool>> predicate)
        {
            return await _context.Servers.FirstOrDefaultAsync(predicate);
        }
        public async Task<PaginatedList<Server>> GetListAsync(Expression<Func<Server, bool>>? predicate = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Servers.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<Server>(items, count, pageIndex, pageSize);
        }
        public async Task<bool> AnyAsync(Expression<Func<Server, bool>> predicate)
        {
            return await _context.Servers.AnyAsync(predicate);
        }
        public async Task AddAsync(Server entity)
        {
            await _context.Servers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Server> entities)
        {
            await _context.Servers.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Server entity)
        {
            _context.Servers.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<Server> entities)
        {
            _context.Servers.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Server entity)
        {
            _context.Servers.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(IEnumerable<Server> entities)
        {
            _context.Servers.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
