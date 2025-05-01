#region code: fatih.unal date: 2025-04-21T09:55:06
using AutoMapper;
using FthAdmin.Application.Features.Servers.Commands;
using FthAdmin.Application.Features.Servers.DTOs;
using FthAdmin.Application.Features.Servers.Queries;
using FthAdmin.Domain.Entities;

namespace FthAdmin.Application.Features.Servers.Profiles
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<Server, ServerDto>();
            CreateMap<CreateServerCommand, Server>()
                .ConstructUsing(src => new Server(
                    src.Name,
                    new FthAdmin.Domain.ValueObjects.IpAddress(src.IpAddress),
                    src.OperatingSystem,
                    src.Description
                ));
        }
    }
}
#endregion
