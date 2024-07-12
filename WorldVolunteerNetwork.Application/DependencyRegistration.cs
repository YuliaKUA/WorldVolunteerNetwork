using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorldVolunteerNetwork.Application.Services;

namespace WorldVolunteerNetwork.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<PostsService>();
            services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);


            return services;
        }


    }
}
