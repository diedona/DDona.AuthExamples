using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AuthExampleDbContext : DbContext
    {
        public AuthExampleDbContext(DbContextOptions options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveMaxLength(400);

            configurationBuilder.Properties<string?>()
                .HaveMaxLength(400);

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureReferenceClass).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
