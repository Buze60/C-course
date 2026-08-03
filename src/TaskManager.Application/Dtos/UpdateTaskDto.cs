using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

/// <summary>
/// Input for editing an existing task. Same shape as <see cref="CreateTaskDto"/>,
/// kept as a separate record so the two use-cases can evolve independently.
/// </summary>
public sealed record UpdateTaskDto(
    string Title,
    string Description,
    TaskPriority Priority,
    DateTime? DueDate);
