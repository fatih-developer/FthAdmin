// code: fatih.unal date: 2025-04-21T14:10:00
using MediatR;

namespace FthAdmin.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResultDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
