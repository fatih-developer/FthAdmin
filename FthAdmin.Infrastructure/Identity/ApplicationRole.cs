#region code:fatih.unal date: 2025-04-21T09:19:56
using Microsoft.AspNetCore.Identity;

namespace FthAdmin.Infrastructure.Identity
{
    /// <summary>
    /// Kimlik yönetimi için genişletilmiş rol nesnesi.
    /// </summary>
    public class ApplicationRole : IdentityRole<int>
    {
        // Ek rol alanları buraya eklenebilir
        public string? Description { get; set; }
    }
}
#endregion
