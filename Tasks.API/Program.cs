using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Tasks.API.Controllers;
using Tasks.App.Servicos.Tasks;
using Tasks.App.Servicos.Tasks.Interfaces;
using Tasks.Infra.Context;
using Tasks.Infra.Tasks.Repositories;
using Tasks.Infra.Tasks.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddDbContext<TasksDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDB"));
});

builder.Services.AddScoped<ITasksAppServico, TasksAppService>();
builder.Services.AddScoped<ITasksRespository, TasksRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

TasksController.Map(app);

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}