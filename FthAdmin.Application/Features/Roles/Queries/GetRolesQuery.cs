// code: fatih.unal date: 2025-04-22
using MediatR;
using System.Collections.Generic;

namespace FthAdmin.Application.Features.Roles.Queries
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }

    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
