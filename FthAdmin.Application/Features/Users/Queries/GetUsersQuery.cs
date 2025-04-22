// code: fatih.unal date: 2025-04-22
using MediatR;
using System.Collections.Generic;

namespace FthAdmin.Application.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
