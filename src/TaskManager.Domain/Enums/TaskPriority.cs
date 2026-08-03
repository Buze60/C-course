namespace TaskManager.Domain.Enums;

/// <summary>
/// How important a task is. The numeric value matters:
/// the larger the number, the more important the task.
/// </summary>
public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
}
