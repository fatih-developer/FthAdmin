#region code: fatih.unal date: 2025-04-21T09:55:06
using FthAdmin.Application.Features.Servers.DTOs;
using MediatR;

namespace FthAdmin.Application.Features.Servers.Queries
{
    public class GetServerByIdQuery : IRequest<ServerDto>
    {
        public int Id { get; set; }
    }
}
#endregion
