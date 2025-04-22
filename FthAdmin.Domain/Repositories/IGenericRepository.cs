#region code: fatih.unal date: 2025-04-21T09:49:32
using FthAdmin.Core.Common;
using System.Linq.Expressions;


namespace FthAdmin.Domain.Repositories
{
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PaginatedList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, int pageIndex = 1, int pageSize = 10);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }

    // Sayfalama için yardımcı sınıf
    public class PaginatedList<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public PaginatedList(IReadOnlyList<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
#endregion
