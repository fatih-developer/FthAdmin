#region code:fatih.unal date: 2025-04-21T09:19:56
using Microsoft.AspNet.Identity.EntityFramework;
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
