// code: fatih.unal date: 2025-04-21T10:06:55
using System;
using System.Collections.Generic;

namespace FthAdmin.Core.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> Roles { get; set; }
    }
}
