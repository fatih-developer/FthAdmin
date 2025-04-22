// code: fatih.unal date: 2025-04-21T14:10:00
using MediatR;

namespace FthAdmin.Application.Features.Auth.Queries
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
