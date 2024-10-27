using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Accounts;
using WorldVolunteerNetwork.Application.Features.Organizers;
using WorldVolunteerNetwork.Application.Features.Organizers.GetPhoto;
using WorldVolunteerNetwork.Application.Features.Posts;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication;
using WorldVolunteerNetwork.Infrastructure.ClientServices;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using WorldVolunteerNetwork.Infrastructure.Options;
using WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetOrganizer;
using WorldVolunteerNetwork.Infrastructure.Queries.Posts;
using WorldVolunteerNetwork.Infrastructure.Repositories;
using JwtProvider = WorldVolunteerNetwork.Infrastructure.ClientServices.JwtProvider;

namespace WorldVolunteerNetwork.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDataStorages(configuration)
                .AddRepositories()
                .AddQueries()
                .AddProviders();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostRepository>();
            services.AddScoped<IOrganizersRepository, OrganizerRepository>();
            services.AddScoped<IVolunteerApplicationRepository, VolunteerApplicationRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();

            return services;
        }

        private static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IMinioProvider, MinioProvider>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            
            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<GetPostsQuery>();
            services.AddScoped<GetAllPostsQuery>();
            services.AddScoped<GetAllOrganizerPhotosQuery>();
            services.AddScoped<GetOrganizerByIdQuery>();

            return services;
        }

        private static IServiceCollection AddDataStorages(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IWorldVolunteerNetworkWriteDbContext, WorldVolunteerNetworkWriteDbContext>();
            services.AddScoped<WorldVolunteerNetworkWriteDbContext>();
            services.AddScoped<WorldVolunteerNetworkReadDbContext>();

            services.AddSingleton<SqlConnectionFactory>();

            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.Minio)
                    .Get<MinioOptions>() ?? throw new Exception("Minio configuration not found");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(false);

            });

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));

            return services;
        }
    }
}
