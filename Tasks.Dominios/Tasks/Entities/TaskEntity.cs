namespace Task.Dominios.Tasks.Entities;

public class TaskEntity
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public TaskEntity(string description, bool completed)
    {
        Description = description;
        Completed = completed;
    }
}