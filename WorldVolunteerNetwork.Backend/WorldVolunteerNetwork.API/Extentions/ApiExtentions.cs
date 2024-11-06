using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorldVolunteerNetwork.Infrastructure.Options;

namespace WorldVolunteerNetwork.API.Extentions
{
    public static class ApiExtentions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new()
                {
                    {new()
                        {
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Reference = new()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>() ?? throw new ApplicationException("Wrong configuration");
                    var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.SecretKey));
                    
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = symmetricKey,
                    };
                });

            services.AddAuthorization();
        }
    }
}
