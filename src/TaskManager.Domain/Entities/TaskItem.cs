using System.Text.Json.Serialization;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

/// <summary>
/// An <em>entity</em> is a class that has an identity (<see cref="Id"/>) and lives
/// longer than a single request. It owns its data AND its behaviour.
///
/// Notice it protects itself: you cannot set an empty title, and you cannot put a
/// completed task back to 'in progress'. That is called <b>encapsulation</b>.
///
/// This is the Domain layer: pure C# with ZERO framework dependencies.
/// </summary>
public sealed class TaskItem
{
    public const int MaxTitleLength = 120;

    // [JsonInclude] tells System.Text.Json to read/write properties whose
    // setter is private. Without it the serializer would silently skip them.
    [JsonInclude]
    public Guid Id { get; private set; }

    // The = string.Empty initialisers exist only to satisfy the compiler's
    // nullability analysis for the JSON deserialization constructor below.
    [JsonInclude]
    public string Title { get; private set; } = string.Empty;
    [JsonInclude]
    public string Description { get; private set; } = string.Empty;
    [JsonInclude]
    public TaskPriority Priority { get; private set; }
    [JsonInclude]
    public TaskState Status { get; private set; }
    [JsonInclude]
    public DateTime? DueDate { get; private set; }
    [JsonInclude]
    public DateTime CreatedAt { get; private set; }
    [JsonInclude]
    public DateTime UpdatedAt { get; private set; }

    // Private parameterless constructor used ONLY by System.Text.Json when it
    // materialises a task back from the JSON file. The [JsonConstructor]
    // attribute is required because the serializer ignores non-public
    // constructors by default. This keeps the public API safe and encapsulated.
    [JsonConstructor]
    private TaskItem() { }

    private TaskItem(string title, string description, TaskPriority priority, DateTime? dueDate)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        Description = description ?? string.Empty;
        Priority = priority;
        Status = TaskState.Pending;
        DueDate = dueDate;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    /// <summary>
    /// The only way a task can be born. A factory method (instead of a public
    /// constructor) makes it obvious that a task starts with these values.
    /// </summary>
    public static TaskItem Create(string title, string description, TaskPriority priority, DateTime? dueDate)
        => new(title, description, priority, dueDate);

    public void Update(string title, string description, TaskPriority priority, DateTime? dueDate)
    {
        SetTitle(title);
        Description = description ?? string.Empty;
        Priority = priority;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkInProgress()
    {
        if (Status == TaskState.Completed)
        {
            throw new InvalidOperationException(
                $"Task '{Title}' is completed and cannot go back to '{TaskState.InProgress}'.");
        }

        Status = TaskState.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = TaskState.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }

        if (title.Trim().Length > MaxTitleLength)
        {
            throw new ArgumentException($"Title cannot be longer than {MaxTitleLength} characters.", nameof(title));
        }

        Title = title.Trim();
    }
}
