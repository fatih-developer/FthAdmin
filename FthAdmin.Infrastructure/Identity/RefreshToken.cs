// code: fatih.unal date: 2025-04-22T08:37:32
using System;

namespace FthAdmin.Infrastructure.Identity
{
    /// <summary>
    /// Kullanıcıya ait refresh token bilgisini tutar.
    /// </summary>
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
