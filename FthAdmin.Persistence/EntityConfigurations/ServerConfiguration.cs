// code: fatih.unal date: 2025-04-21T10:08:39
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FthAdmin.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FthAdmin.Domain.ValueObjects;

namespace FthAdmin.Persistence.EntityConfigurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IpAddress)
                .IsRequired()
                .HasConversion(new ValueConverter<FthAdmin.Domain.ValueObjects.IpAddress, string>(
                    v => v.Value,
                    v => new FthAdmin.Domain.ValueObjects.IpAddress(v)
                ));
            builder.Property(s => s.OperatingSystem).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Hostname).HasMaxLength(100);
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.LastStatusCheck);
            builder.Property(s => s.Description).HasMaxLength(250); 
            // Diğer ilişkiler ve property konfigürasyonları buraya eklenebilir
        }
    }
}
