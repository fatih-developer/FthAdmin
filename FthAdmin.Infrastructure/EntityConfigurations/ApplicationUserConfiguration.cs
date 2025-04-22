// code: fatih.unal date: 2025-04-21T14:01:00
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FthAdmin.Infrastructure.Identity;

namespace FthAdmin.Infrastructure.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.DisplayName).HasMaxLength(100);
            // Diğer Identity özellikleri ve ilişkiler otomatik olarak yapılandırılır
        }
    }
}
