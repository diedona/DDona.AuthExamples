using Domain.Services.Infrastructure;
using Infrastructure.Services;

namespace WebApi.Extensions.Services
{
    public static class DomainServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IEncryption, BCryptEncryption>();
        }
    }
}
