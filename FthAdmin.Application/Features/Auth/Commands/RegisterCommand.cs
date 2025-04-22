// code: fatih.unal date: 2025-04-21T14:10:00
using MediatR;

namespace FthAdmin.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResultDto>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResultDto
    {
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
