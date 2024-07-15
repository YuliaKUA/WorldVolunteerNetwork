using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorldVolunteerNetwork.Application.Posts.CreatePost;

namespace WorldVolunteerNetwork.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CreatePostsService>();
            
            return services;
        }
    }
}
