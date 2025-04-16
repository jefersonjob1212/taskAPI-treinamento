using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Dominios.Tasks.Entities;

namespace Tasks.Infra.Tasks.Configurations;

public class TasksConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("tasks");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(x => x.Description).HasColumnName("description").IsRequired();
        builder.Property(x => x.Completed).HasColumnName("completed").IsRequired();
    }
}