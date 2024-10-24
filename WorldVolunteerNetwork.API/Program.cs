using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
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

builder.Services.AddHostedService<Cleaner>();

var app = builder.Build();

//Apply all migrations in project || Create new DB
//Analogue "dotnet ef database update"
if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<WorldVolunteerNetworkWriteDbContext>();
    await dbContext.Database.MigrateAsync();

    //var admin = new User("admin", "admin", Role.Admin);
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