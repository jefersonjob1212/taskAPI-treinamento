using Task.Dominios.Tasks.Entities;
using Tasks.App.Servicos.Tasks.Interfaces;
using Tasks.DataTransfer.Tasks.Requests;
using Tasks.DataTransfer.Tasks.Responses;
using Tasks.Infra.Tasks.Repositories.Interfaces;

namespace Tasks.App.Servicos.Tasks;

public class TasksAppService : ITasksAppServico
{
    private readonly ITasksRespository _tasksRepository;

    public TasksAppService(ITasksRespository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<TaskResponse> GetTaskByIdAsync(int id)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(id);
        return new TaskResponse(task.Id, task.Description, task.Completed);
    }

    public async Task<IEnumerable<TaskResponse>> GetAsync(TaskListRequest taskListRequest)
    {
        var tasks = await _tasksRepository.GetAsync(taskListRequest.Description, taskListRequest.Page, taskListRequest.PageSize);
        var list = new List<TaskResponse>();
        foreach (var taskEntity in tasks)
        {
            list.Add(new TaskResponse(taskEntity.Id, taskEntity.Description, taskEntity.Completed));
        }
        return list;
    }

    public System.Threading.Tasks.Task InsertAsync(TaskCreateRequest taskCreateRequest)
    {
        var task = new TaskEntity(taskCreateRequest.Description, taskCreateRequest.Completed);
        return _tasksRepository.InsertAsync(task);
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, TaskUpdateRequest taskUpdateRequest)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(id);
        task.Description = taskUpdateRequest.Description;
        task.Completed = taskUpdateRequest.Completed;
        await _tasksRepository.UpdateAsync(task);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(id);
        await _tasksRepository.DeleteAsync(task);
    }
}