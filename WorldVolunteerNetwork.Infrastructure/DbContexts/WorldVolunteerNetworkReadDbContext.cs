using Contracts.Posts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.DbContexts
{
    public class WorldVolunteerNetworkReadDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WorldVolunteerNetworkReadDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<PostDto> Posts => Set<PostDto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WorldVolunteerNetworkDbContext"));

            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#1//modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WorldVolunteerNetworkReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);
        }
    }
}
