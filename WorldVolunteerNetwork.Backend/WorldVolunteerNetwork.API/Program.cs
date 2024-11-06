using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using WorldVolunteerNetwork.API.Authorization;
using WorldVolunteerNetwork.API.Extentions;
using WorldVolunteerNetwork.API.Middlewares;
using WorldVolunteerNetwork.API.Validation;
using WorldVolunteerNetwork.Application;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;
using WorldVolunteerNetwork.Infrastructure;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Seq(builder.Configuration.GetSection("Seq").Value ?? throw new ApplicationException("Seq configuration not found"))
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

Log.Information("Starting up application");

builder.Services.AddSwagger();
builder.Services.AddControllers();

builder.Services.AddSerilog();

//var config = builder.Configuration;

builder.Host.UseSerilog(Log.Logger);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});


builder.Services.AddHttpLogging(options => { });

builder.Services.AddAuth(builder.Configuration);

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

    //var admin = new User(Email.Create("admin@admin").Value, passwordHash, Role.Admin);
    //await dbContext.Users.AddAsync(admin);
    //await dbContext.SaveChangesAsync();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();
//app.UseHttpLogging();

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