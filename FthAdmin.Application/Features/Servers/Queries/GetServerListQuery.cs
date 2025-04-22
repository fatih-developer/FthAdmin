#region code: fatih.unal date: 2025-04-21T09:55:06
using FthAdmin.Application.Features.Servers.DTOs;
using MediatR;
using System.Collections.Generic;

namespace FthAdmin.Application.Features.Servers.Queries
{
    public class GetServerListQuery : IRequest<List<ServerDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
#endregion
