#region code: fatih.unal date: 2025-04-22
using Microsoft.AspNetCore.Identity;

namespace FthAdmin.Infrastructure.Identity
{
    /// <summary>
    /// Kimlik yönetimi için genişletilmiş kullanıcı nesnesi.
    /// </summary>
    public class ApplicationUser : IdentityUser<int>
    {
        // Ek kullanıcı alanları buraya eklenebilir
        public string? DisplayName { get; set; }
    }
}
#endregion
