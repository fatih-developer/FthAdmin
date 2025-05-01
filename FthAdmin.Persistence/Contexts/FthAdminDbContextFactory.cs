// code: fatih.unal date: 2025-04-24T11:45:49
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FthAdmin.Persistence.Contexts
{
    public class FthAdminDbContextFactory : IDesignTimeDbContextFactory<FthAdminDbContext>
    {
        public FthAdminDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FthAdminDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=FthAdminData;User=sa;Password=Fth1818***;TrustServerCertificate=True;");
            return new FthAdminDbContext(optionsBuilder.Options);
        }
    }
}
