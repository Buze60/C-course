using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

/// <summary>
/// A <b>DTO</b> (Data Transfer Object) is a plain shape used to move data
/// BETWEEN layers. It deliberately has no behaviour and no validation —
/// it is just a bag of properties. We never expose the entity to the UI.
/// </summary>
public sealed record TaskDto(
    Guid Id,
    string Title,
    string Description,
    TaskPriority Priority,
    TaskState Status,
    DateTime? DueDate,
    DateTime CreatedAt,
    DateTime UpdatedAt);
