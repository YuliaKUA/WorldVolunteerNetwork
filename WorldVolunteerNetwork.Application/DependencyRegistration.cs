using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;
using WorldVolunteerNetwork.Application.Features.Organizers.DeletePhoto;
using WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication;

namespace WorldVolunteerNetwork.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
            services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<CreatePostsHandler>();

            services.AddScoped<CreateOrganizersHandler>();
            services.AddScoped<UploadOrganizerPhotoHandler>();
            services.AddScoped<DeleteOrganizerPhotoHandler>();

            services.AddScoped<ApplyVolunteerApplicationHandler>();
            
            return services;
        }
    }
}
