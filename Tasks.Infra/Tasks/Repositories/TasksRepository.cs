using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task.Dominios.Tasks.Entities;
using Tasks.Infra.Context;
using Tasks.Infra.Tasks.Repositories.Interfaces;

namespace Tasks.Infra.Tasks.Repositories;

public class TasksRepository : ITasksRespository
{
    private TasksDbContext _context;

    public TasksRepository(TasksDbContext context)
    {
        _context = context;
    }
    
    public Task<TaskEntity> GetTaskByIdAsync(int id)
    {
        return _context.TaskEntities.FindAsync(id).AsTask();
    }

    public Task<IEnumerable<TaskEntity>> GetAsync(string? description, int? pg, int? size)
    {
        Expression<Func<TaskEntity, bool>> filter = x => true;
        if (!string.IsNullOrWhiteSpace(description))
            filter = x => x.Description.Contains(description);

        return System.Threading.Tasks.Task.FromResult<IEnumerable<TaskEntity>>(_context.TaskEntities.Where(filter));
    }

    public async System.Threading.Tasks.Task InsertAsync(TaskEntity taskEntity)
    {
        await _context.TaskEntities.AddAsync(taskEntity).AsTask();
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskEntity taskEntity)
    {
        await _context.TaskEntities.ExecuteUpdateAsync(
            t => 
                t.SetProperty(s => s.Description, taskEntity.Description));
        await _context.TaskEntities.ExecuteUpdateAsync(
            t => 
                t.SetProperty(s => s.Completed, taskEntity.Completed));
        await _context.SaveChangesAsync();
    }

    public System.Threading.Tasks.Task DeleteAsync(TaskEntity taskEntity)
    {
        _context.TaskEntities.Remove(taskEntity);
        return _context.SaveChangesAsync();
    }
}