using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions.EntityFramework
{
    public static class EntityFrameworkExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<AuthExampleDbContext>(opt =>
            {
                opt.UseSqlServer(configurationManager.GetConnectionString("Default"));
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
