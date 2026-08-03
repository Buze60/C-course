using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

/// <summary>
/// Input for creating a task. Records give us structural equality and
/// a one-line declaration of all the fields.
/// </summary>
public sealed record CreateTaskDto(
    string Title,
    string Description,
    TaskPriority Priority,
    DateTime? DueDate);
