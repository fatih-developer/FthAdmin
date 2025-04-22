// code: fatih.unal date: 2025-04-21T10:07:47
using System;

namespace FthAdmin.Core.Security.JWT
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
