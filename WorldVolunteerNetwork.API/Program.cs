using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.API.Helpers;
using WorldVolunteerNetwork.Application;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Infrastructure;
using WorldVolunteerNetwork.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddScoped<IPostsRepository, PostRepository>();
builder.Services.AddScoped<PostsService>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<WorldVolunteerNetworkDbContext>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<WorldVolunteerNetworkDbContext>();
//    dbContext.Database.Migrate();
//}

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
