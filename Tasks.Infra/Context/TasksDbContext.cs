using Microsoft.EntityFrameworkCore;
using Task.Dominios.Tasks.Entities;
using Tasks.Infra.Tasks.Configurations;

namespace Tasks.Infra.Context;

public class TasksDbContext(DbContextOptions<TasksDbContext> options) : DbContext(options)
{
    public DbSet<TaskEntity> TaskEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TasksConfiguration());
    }
}