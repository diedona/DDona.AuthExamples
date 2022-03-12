using WebApi.Extensions.Configuration;
using WebApi.Extensions.DomainServices;

namespace WebApi
{
    public class Startup
    {
        private ConfigurationManager _ConfigurationManager;

        public Startup(ConfigurationManager configuration)
        {
            _ConfigurationManager = configuration;
        }

        public void ConfigureApp(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAppSettingsConfiguration(_ConfigurationManager);
            services.AddDomainServices();
        }
    }
}
