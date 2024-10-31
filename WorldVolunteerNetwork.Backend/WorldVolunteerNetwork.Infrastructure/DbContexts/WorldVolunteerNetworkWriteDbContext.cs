using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.DbContexts
{
    public class WorldVolunteerNetworkWriteDbContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        public WorldVolunteerNetworkWriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Organizer> Organizers => Set<Organizer>();
        //public DbSet<OrganizerPhoto> Photos => Set<OrganizerPhoto>();
        //public DbSet<PostPhoto> PostPhotos => Set<PostPhoto>();
        public DbSet<VolunteerApplication> volunteerApplications => Set<VolunteerApplication>();
        public DbSet<User> Users => Set<User>();

        //public DbSet<SocialMedia> SocialMedia => Set<SocialMedia>();

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
