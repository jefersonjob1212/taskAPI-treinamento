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
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDB"), b => b.MigrationsAssembly("Tasks.Infra"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<ITasksAppServico, TasksAppService>();
builder.Services.AddScoped<ITasksRespository, TasksRepository>();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

TasksController.Map(app);

app.Run();
