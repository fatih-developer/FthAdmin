// code: fatih.unal date: 2025-04-21T14:12:30
using FthAdmin.Application.Features.Auth.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FthAdmin.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
            // Application katmanındaki servisleri burada DI'a ekleyebilirsin
            // örn: MediatR, AutoMapper, Validation vb.
            return services;
        }
    }
}
