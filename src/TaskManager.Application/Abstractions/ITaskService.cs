using TaskManager.Application.Dtos;

namespace TaskManager.Application.Abstractions;

/// <summary>
/// The application "brain". It contains the business rules and orchestrates
/// the repository. The UI layer talks to this interface and nothing else.
/// </summary>
public interface ITaskService
{
    Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default);
    Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken = default);
    Task CompleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task MarkInProgressAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
