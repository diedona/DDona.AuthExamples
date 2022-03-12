using Domain.Services.Domain;

namespace WebApi.Extensions.Services
{
    public static class InfrastructureServicesExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
        }
    }
}
