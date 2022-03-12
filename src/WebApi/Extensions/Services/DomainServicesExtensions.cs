using Domain.Services.Domain;
using Domain.Services.Infrastructure;
using Infrastructure.Services;

namespace WebApi.Extensions.Services
{
    public static class InfrastructureServicesExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        }
    }
}
