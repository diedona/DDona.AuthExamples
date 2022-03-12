using Domain.Repositories;
using Infrastructure.Repositories;

namespace WebApi.Extensions.Repositories
{
    public static class RepositoresExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}
