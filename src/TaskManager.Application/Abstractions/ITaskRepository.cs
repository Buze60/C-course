using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

/// <summary>
/// The <b>repository pattern</b>: an abstraction over "where data lives".
/// The Application layer only knows THIS interface — never the JSON file,
/// never SQL, never anything concrete.
///
/// If we later want SQL Server, we write one new class that implements this
/// interface and change a single line in the DI container. Nothing else changes.
/// </summary>
public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
