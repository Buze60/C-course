namespace TaskManager.Application.Exceptions;

/// <summary>
/// Thrown when a caller asks for a task that does not exist.
/// </summary>
public sealed class TaskNotFoundException : Exception
{
    public Guid TaskId { get; }

    public TaskNotFoundException(Guid taskId)
        : base($"Task with id '{taskId}' was not found.")
    {
        TaskId = taskId;
    }
}
