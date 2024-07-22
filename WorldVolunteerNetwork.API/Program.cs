using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using WorldVolunteerNetwork.API.Middlewares;
using WorldVolunteerNetwork.API.Validation;
using WorldVolunteerNetwork.Application;
using WorldVolunteerNetwork.Infrastructure;

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


builder.Services.AddHttpLogging(option => { });

var app = builder.Build();

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
