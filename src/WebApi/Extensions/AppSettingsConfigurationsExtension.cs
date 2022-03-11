using WebApi.Models;

namespace WebApi.Extensions
{
    public static class AppSettingsConfigurationsExtension
    {
        public static void AddAppSettingsConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<GeneralConfiguration>(configurationManager.GetSection(nameof(GeneralConfiguration)));
        }
    }
}
