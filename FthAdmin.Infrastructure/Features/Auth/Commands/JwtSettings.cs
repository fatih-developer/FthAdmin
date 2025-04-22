// code: fatih.unal date: 2025-04-22T08:32:25
namespace FthAdmin.Infrastructure.Features.Auth.Commands
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpireMinutes { get; set; }
    }
}
