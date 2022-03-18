using Domain.Services.Application;
using Domain.Services.Domain;

namespace WebApi.Extensions.Services
{
    public static class DomainServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationApplicationService>();
            services.AddScoped<AuthenticationDomainService>();
        }
    }
}
