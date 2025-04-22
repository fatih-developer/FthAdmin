// code: fatih.unal date: 2025-04-21T10:06:55
using System.Collections.Generic;

namespace FthAdmin.Core.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(int userId, string userName, IList<string> roles);
        bool ValidateToken(string token, out int userId, out IList<string> roles);
    }
}
