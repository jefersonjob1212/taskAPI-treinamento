using Tasks.DataTransfer.Tasks.Requests;
using Tasks.DataTransfer.Tasks.Responses;

namespace Tasks.App.Servicos.Tasks.Interfaces;

public interface ITasksAppServico
{
    Task<TaskResponse> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskResponse>> GetAsync(TaskListRequest taskListRequest);
    System.Threading.Tasks.Task InsertAsync(TaskCreateRequest taskCreateRequest);
    System.Threading.Tasks.Task UpdateAsync(int id, TaskUpdateRequest taskUpdateRequest);
    System.Threading.Tasks.Task DeleteAsync(int id);
}