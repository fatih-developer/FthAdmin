// code: fatih.unal date: 2025-04-22T09:01:40
using MediatR;

namespace FthAdmin.Application.Features.Auth.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
