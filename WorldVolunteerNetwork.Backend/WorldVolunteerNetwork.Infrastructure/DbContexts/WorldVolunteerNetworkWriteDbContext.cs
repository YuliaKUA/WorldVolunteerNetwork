using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
<<<<<<<< HEAD:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/DbContexts/WorldVolunteerNetworkWriteDbContext.cs
using WorldVolunteerNetwork.Application.Abstractions;
========
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>>> origin/main:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/WorldVolunteerNetworkDbContext.cs
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.Interceptors;

namespace WorldVolunteerNetwork.Infrastructure
{
<<<<<<<< HEAD:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/DbContexts/WorldVolunteerNetworkWriteDbContext.cs
    public class WorldVolunteerNetworkWriteDbContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly CacheInvalidationInterceptor _cacheInvalidationInterceptor;
        public WorldVolunteerNetworkWriteDbContext(
            IConfiguration configuration,
            CacheInvalidationInterceptor cacheInvalidationInterceptor)
========
    public class WorldVolunteerNetworkDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WorldVolunteerNetworkDbContext(IConfiguration configuration)
>>>>>>>> origin/main:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/WorldVolunteerNetworkDbContext.cs
        {
            _configuration = configuration;
            _cacheInvalidationInterceptor = cacheInvalidationInterceptor;
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
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(WorldVolunteerNetworkDbContext)));

            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.AddInterceptors(_cacheInvalidationInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#1//modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorldVolunteerNetworkDbContext).Assembly);
        }
    }
}