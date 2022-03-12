namespace WebApi.Models.Configurations
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int LifeSpanMinutes { get; set; }

        public TimeSpan LifeSpan => TimeSpan.FromMinutes(LifeSpanMinutes);
    }
}
