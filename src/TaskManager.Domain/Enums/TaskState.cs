namespace TaskManager.Domain.Enums;

/// <summary>
/// The lifecycle of a task.
///
/// Named <c>TaskState</c> on purpose: <c>System.Threading.Tasks.TaskStatus</c>
/// is a built-in .NET type, and naming ours the same would cause an ambiguity
/// error in every file that imports both namespaces.
/// </summary>
public enum TaskState
{
    Pending = 1,
    InProgress = 2,
    Completed = 3,
}
