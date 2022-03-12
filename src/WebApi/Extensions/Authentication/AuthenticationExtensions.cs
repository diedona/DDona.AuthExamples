using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Models.Configurations;

namespace WebApi.Extensions.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddProjectAuthentication(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var jwtConfiguration = new JwtConfiguration();
            configurationManager.GetSection(nameof(JwtConfiguration)).Bind(jwtConfiguration);

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.FromSeconds(0),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.ValidIssuer,
                        ValidAudience = jwtConfiguration.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
                    };
                });
        }
    }
}
