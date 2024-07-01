using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure
{
    public class WorldVolunteerNetworkDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WorldVolunteerNetworkDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Post> Posts => Set<Post>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(WorldVolunteerNetworkDbContext)));

            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#1//modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorldVolunteerNetworkDbContext).Assembly);
        }
    }
}
