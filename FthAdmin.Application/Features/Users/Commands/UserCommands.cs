// code: fatih.unal date: 2025-04-22
using MediatR;

namespace FthAdmin.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AssignRoleCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }

    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
