namespace Tasks.DataTransfer.Tasks.Requests;

public class TaskListRequest
{
    public string? Description { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public TaskListRequest()
    {
        Page = 1;
        PageSize = 10;
    }

    public TaskListRequest(string? description, int? page, int? pageSize)
    {
        Description = description;
        Page = page ?? 1;
        PageSize = pageSize ?? 10;
    }
}