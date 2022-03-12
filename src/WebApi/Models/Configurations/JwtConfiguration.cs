namespace WebApi.Models.Configurations
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public int LifeSpanMinutes { get; set; }

        public TimeSpan LifeSpan => TimeSpan.FromMinutes(LifeSpanMinutes);
    }
}
