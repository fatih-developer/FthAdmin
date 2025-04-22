#region code:fatih.unal date: 2025-04-21T09:27:12
using System;
using FthAdmin.Domain.ValueObjects;
using FthAdmin.Domain.Enums;
using FthAdmin.Core.Common;

namespace FthAdmin.Domain.Entities
{
    public class Server : BaseEntity<Guid>
    {
        protected Server() { }

        public Server(string name, IpAddress ipAddress, string operatingSystem)
        {
            Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Server name cannot be null or empty.", nameof(name));
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));
            if (string.IsNullOrWhiteSpace(operatingSystem))
                throw new ArgumentException("Operating System cannot be null or empty.", nameof(operatingSystem));
            Name = name;
            IpAddress = ipAddress;
            OperatingSystem = operatingSystem;
            Status = ServerStatus.Unknown;
            LastStatusCheck = null;
        }

        public string Name { get; private set; }
        public IpAddress IpAddress { get; private set; }
        public string Hostname { get; private set; }
        public string OperatingSystem { get; private set; }
        public ServerStatus Status { get; private set; }
        public DateTime? LastStatusCheck { get; private set; }

        public void UpdateStatus(ServerStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                LastStatusCheck = DateTime.UtcNow;
            }
        }
        public void UpdateHostname(string hostname)
        {
            Hostname = hostname;
        }
        public void UpdateIpAddress(string newIpAddress)
        {
            var newAddress = new IpAddress(newIpAddress);
            IpAddress = newAddress;
        }
    }
}
#endregion
