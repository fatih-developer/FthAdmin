#region code: fatih.unal date: 2025-04-21T09:56:09
namespace FthAdmin.Application.Features.Servers.DTOs
{
    public class ServerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public string Hostname { get; set; }
        public string Status { get; set; }
        public string LastStatusCheck { get; set; }
    }
}
#endregion
