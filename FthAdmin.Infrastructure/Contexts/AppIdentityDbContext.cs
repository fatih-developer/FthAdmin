// code: fatih.unal date: 2025-04-22T08:37:32
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FthAdmin.Infrastructure.Identity;

namespace FthAdmin.Infrastructure.Contexts
{
    // Sadece Identity tabloları için kullanılacak context
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EntityConfigurations.RefreshTokenConfiguration());
        }
    }
}
