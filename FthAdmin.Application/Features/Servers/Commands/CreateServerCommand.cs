#region code: fatih.unal date: 2025-04-21T09:55:06
using MediatR;

namespace FthAdmin.Application.Features.Servers.Commands
{
    public class CreateServerCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
    }
}
#endregion
