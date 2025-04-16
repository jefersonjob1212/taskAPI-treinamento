using Task.Dominios.Tasks.Entities;

namespace Tasks.Infra.Tasks.Repositories.Interfaces;

public interface ITasksRespository
{
    Task<TaskEntity> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetAsync(string? description, int? pg, int? size);
    System.Threading.Tasks.Task InsertAsync(TaskEntity taskEntity);
    System.Threading.Tasks.Task UpdateAsync(TaskEntity taskEntity);
    System.Threading.Tasks.Task DeleteAsync(TaskEntity taskEntity);
}