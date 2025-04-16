using Microsoft.AspNetCore.Mvc;
using Tasks.App.Servicos.Tasks.Interfaces;
using Tasks.DataTransfer.Tasks.Requests;

namespace Tasks.API.Controllers;

public class TasksController
{
    public static void Map(WebApplication app)
    {
        app.MapGet("api/v1/tasks", async (
            string? description, 
            int? page, 
            int? pageSize,
            ITasksAppServico tasksAppServico) =>
        {
            var request = new TaskListRequest(description, page, pageSize);
            return await tasksAppServico.GetAsync(request);
        });

        app.MapGet("api/v1/tasks/{id}", async (int id, ITasksAppServico tasksAppServico) =>
             await tasksAppServico.GetTaskByIdAsync(id));

        app.MapPost("api/v1/tasks", async ([FromBody] TaskCreateRequest request, ITasksAppServico tasksAppServico) =>
        {
            await tasksAppServico.InsertAsync(request);
        });

        app.MapPut("api/v1/tasks/{id}",
            async (int id, [FromBody] TaskUpdateRequest request, ITasksAppServico tasksAppServico) =>
            {
                await tasksAppServico.UpdateAsync(id, request);
            });

        app.MapDelete("api/v1/tasks/{id}", async (int id, ITasksAppServico tasksAppServico) =>
        {
            await tasksAppServico.DeleteAsync(id);
        });
    }
}