// code: fatih.unal date: 2025-04-22
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FthAdmin.Application.Features.Roles.Commands;
using FthAdmin.Application.Features.Roles.Queries;
using System.Collections.Generic;
using FthAdmin.Application.Abstractions;

namespace FthAdmin.Application.Features.Roles.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
    {
        private readonly IRoleService _roleService;
        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.CreateRoleAsync(request.RoleName);
        }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleService _roleService;
        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.DeleteRoleAsync(request.RoleId);
        }
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly IRoleService _roleService;
        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetRolesAsync();
        }
    }
}
