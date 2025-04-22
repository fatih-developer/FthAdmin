// code: fatih.unal date: 2025-04-21T14:01:00
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FthAdmin.Infrastructure.Identity;

namespace FthAdmin.Infrastructure.EntityConfigurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Description).HasMaxLength(200);
            // Diğer Identity özellikleri ve ilişkiler otomatik olarak yapılandırılır
        }
    }
}
