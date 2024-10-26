using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text;
using WorldVolunteerNetwork.API.Middlewares;
using WorldVolunteerNetwork.API.Validation;
using WorldVolunteerNetwork.Application;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
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
        var key = "secretKeyFromConfigurationKeyKey";
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = symmetricKey,
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddHostedService<Cleaner>();

var app = builder.Build();

//Apply all migrations in project || Create new DB
//Analogue "dotnet ef database update"
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