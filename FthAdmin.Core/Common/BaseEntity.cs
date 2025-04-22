#region code:fatih.unal date: 2025-04-21T10:04:36

namespace FthAdmin.Core.Common
{
    /// <summary>
    /// Tüm Entityler için temel sınıf.
    /// </summary>
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
#endregion
