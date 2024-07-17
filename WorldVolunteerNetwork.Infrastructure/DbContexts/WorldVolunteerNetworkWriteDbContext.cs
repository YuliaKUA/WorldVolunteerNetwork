using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.DbContexts
{
    public class WorldVolunteerNetworkWriteDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WorldVolunteerNetworkWriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Organizer> Organizers => Set<Organizer>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WorldVolunteerNetworkDbContext"));

            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#1//modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WorldVolunteerNetworkWriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }
    }
}
