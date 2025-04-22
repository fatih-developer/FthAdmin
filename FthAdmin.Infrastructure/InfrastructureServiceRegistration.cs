// code: fatih.unal date: 2025-04-22T09:01:40
using FthAdmin.Infrastructure.Contexts;
using FthAdmin.Infrastructure.Features.Auth.Commands;
using FthAdmin.Infrastructure.Identity;
using FthAdmin.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FthAdmin.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Docker SQL Server bağlantısı
            services.AddDbContext<FthAdminDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Identity için ayrı context kaydı
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // JWT ayarlarını DI'a ekle
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // MediatR handler kayıtları
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RefreshTokenCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RevokeRefreshTokenCommandHandler).Assembly));
            return services;
        }
    }
}
