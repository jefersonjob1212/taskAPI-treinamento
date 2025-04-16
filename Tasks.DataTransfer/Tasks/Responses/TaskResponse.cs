namespace Tasks.DataTransfer.Tasks.Responses;

public class TaskResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public TaskResponse(int id, string description, bool completed)
    {
        Id = id;
        Description = description;
        Completed = completed;
    }
}