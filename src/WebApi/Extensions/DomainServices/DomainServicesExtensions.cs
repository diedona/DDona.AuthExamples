using Domain.Services.Domain;

namespace WebApi.Extensions.DomainServices
{
    public static class DomainServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<LoginService>();
        }
    }
}
