// code: fatih.unal date: 2025-04-21T10:06:55
namespace FthAdmin.Core.Security.JWT
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int AccessTokenExpiration { get; set; }
    }
}
