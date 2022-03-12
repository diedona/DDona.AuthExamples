using WebApi.Models.Configurations;

namespace WebApi.Extensions.Configuration
{
    public static class AppSettingsConfigurationsExtension
    {
        public static void AddAppSettingsConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<GeneralConfiguration>(configurationManager.GetSection(nameof(GeneralConfiguration)));
            services.Configure<JwtConfiguration>(configurationManager.GetSection(nameof(JwtConfiguration)));
        }
    }
}
