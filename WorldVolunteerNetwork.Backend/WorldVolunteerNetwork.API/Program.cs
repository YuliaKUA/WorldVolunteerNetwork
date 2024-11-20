using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using WorldVolunteerNetwork.Infrastructure.Kafka;

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
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<KafkaMessageProducer>();
builder.Services.AddHostedService<KafkaMessageConsumer>();

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

            policy
            .WithOrigins("http://localhost:5000")
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

app.MapPost("kafka", 
    async (
        [FromQuery] string topic,
        [FromBody] string message,
        KafkaMessageProducer producer) =>
{
    //send message to kafka
    await producer.Publish(topic, message);
});

//Create topic+partition
var kafkaConfig = new AdminClientConfig()
{
    BootstrapServers = "172.18.0.9:9092",
};

using var kafkaAdminClient = new AdminClientBuilder(kafkaConfig).Build();
var metaData = kafkaAdminClient.GetMetadata(TimeSpan.FromSeconds(5));
var topic = metaData.Topics.FirstOrDefault(t => t.Topic == "test-topic");
if(topic is null)
{
    var topicSpecification = new TopicSpecification() 
    {
        Name = "test-topic",
        NumPartitions = 2,
    };

    await kafkaAdminClient.CreateTopicsAsync([topicSpecification]);
}

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