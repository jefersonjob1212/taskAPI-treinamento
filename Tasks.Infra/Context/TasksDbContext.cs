using Microsoft.EntityFrameworkCore;
using Task.Dominios.Tasks.Entities;
using Tasks.Infra.Tasks.Configurations;

namespace Tasks.Infra.Context;

public class TasksDbContext : DbContext
{
    public DbSet<TaskEntity> TaskEntities { get; set; }

    public TasksDbContext(DbContextOptions<TasksDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TasksConfiguration());
    }
}