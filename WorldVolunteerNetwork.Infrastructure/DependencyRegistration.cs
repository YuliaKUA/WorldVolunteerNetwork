using Microsoft.Extensions.DependencyInjection;
using WorldVolunteerNetwork.Application.Features.Organizers;
using WorldVolunteerNetwork.Application.Features.Posts;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using WorldVolunteerNetwork.Infrastructure.Queries.Posts;
using WorldVolunteerNetwork.Infrastructure.Repositories;

namespace WorldVolunteerNetwork.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddDatabase()
                .AddRepositories()
                .AddQueries();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostRepository>();
            services.AddScoped<IOrganizersRepository, OrganizerRepository>();

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<GetPostsQuery>();
            services.AddScoped<GetAllPostsQuery>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddScoped<WorldVolunteerNetworkWriteDbContext>();
            services.AddScoped<WorldVolunteerNetworkReadDbContext>();

            services.AddSingleton<SqlConnectionFactory>();

            return services;
        }
    }
}
