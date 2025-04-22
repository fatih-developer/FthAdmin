// code: fatih.unal date: 2025-04-22
using MediatR;

namespace FthAdmin.Application.Features.Roles.Commands
{
    public class CreateRoleCommand : IRequest<string>
    {
        public string RoleName { get; set; }
    }

    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }
}
