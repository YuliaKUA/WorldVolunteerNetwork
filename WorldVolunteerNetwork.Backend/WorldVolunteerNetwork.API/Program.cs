using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text;
using WorldVolunteerNetwork.API.Authorization;
using WorldVolunteerNetwork.API.Middlewares;
using WorldVolunteerNetwork.API.Validation;
using WorldVolunteerNetwork.Application;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using WorldVolunteerNetwork.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
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
        {
            new()
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
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});


builder.Services.AddHttpLogging(options => { });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>() ?? throw new ApplicationException("Wrong configuration");
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.SecretKey));

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = symmetricKey,
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionsAuthorizationsHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

///policy role based checks
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("organizers.create", policyBuilder =>
//    {
//        policyBuilder.RequireClaim("Permissions", "organizers.create");
//    });
//});

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

builder.Services.AddHostedService<Cleaner>();

var app = builder.Build();

///Apply all migrations in project || Create new DB
///Analogue "dotnet ef database update"
if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<WorldVolunteerNetworkWriteDbContext>();
    await dbContext.Database.MigrateAsync();

    //var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("admin");

    //var admin = new User("admin", passwordHash, Role.Admin);
    //await dbContext.Users.AddAsync(admin);
    //await dbContext.SaveChangesAsync();
}



app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpLogging();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<WorldVolunteerNetworkDbContext>();
//    dbContext.Database.Migrate();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


public class Cleaner : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine(".....");
            await Task.Delay(3000, stoppingToken);
        }
    }
}