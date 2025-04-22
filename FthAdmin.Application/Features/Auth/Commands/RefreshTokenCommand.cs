// code: fatih.unal date: 2025-04-22T09:00:23
using MediatR;

namespace FthAdmin.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResultDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
