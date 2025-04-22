// code: fatih.unal date: 2025-04-21T10:10:44
using Microsoft.EntityFrameworkCore;
using FthAdmin.Domain.Entities;

namespace FthAdmin.Persistence.Contexts
{
    // ApplicationUser ve ApplicationRole Persistence katmanında kullanılmamalı, sadece Infrastructure'da olmalı.
    // Eğer IdentityDbContext kullanmak istiyorsan, bu DbContext'i Infrastructure katmanına taşımalısın.
    // Persistence katmanında sadece saf entity'ler ve genel DbContext olmalı.
    public class FthAdminDbContext : DbContext
    {
        public FthAdminDbContext(DbContextOptions<FthAdminDbContext> options) : base(options) { }

        public DbSet<Server> Servers { get; set; }
        // Diğer DbSet'ler buraya eklenebilir

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FthAdminDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
