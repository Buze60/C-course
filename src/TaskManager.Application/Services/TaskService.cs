using Microsoft.Extensions.Logging;
using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

/// <summary>
/// The application service: it orchestrates the domain entities and the
/// repository, applies business rules, translates between entities and DTOs,
/// and logs what happened.
///
/// It depends only on ABSTRACTIONS (ITaskRepository) and is therefore easy to
/// unit test with a fake repository.
/// </summary>
public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _repository.GetAllAsync(cancellationToken);

        return tasks
            .OrderByDescending(task => task.CreatedAt)
            .Select(ToDto)
            .ToList();
    }

    public async Task<TaskDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await FindAsync(id, cancellationToken);
        return ToDto(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Let the entity decide what "a valid new task" means.
            var task = TaskItem.Create(dto.Title, dto.Description, dto.Priority, dto.DueDate);

            await _repository.AddAsync(task, cancellationToken);

            _logger.LogInformation(
                "Created task '{Title}' with id {TaskId}",
                task.Title,
                task.Id);

            return ToDto(task);
        }
        catch (ArgumentException ex)
        {
            // Domain validation errors become an application-level exception
            // the UI knows how to show to the user.
            throw new InvalidTaskException(ex.Message, ex);
        }
    }

    public async Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var task = await FindAsync(id, cancellationToken);

        try
        {
            task.Update(dto.Title, dto.Description, dto.Priority, dto.DueDate);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidTaskException(ex.Message, ex);
        }

        await _repository.UpdateAsync(task, cancellationToken);

        _logger.LogInformation("Updated task '{Title}' ({TaskId})", task.Title, task.Id);

        return ToDto(task);
    }

    public async Task CompleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await FindAsync(id, cancellationToken);

        task.Complete();
        await _repository.UpdateAsync(task, cancellationToken);

        _logger.LogInformation("Completed task '{Title}' ({TaskId})", task.Title, task.Id);
    }

    public async Task MarkInProgressAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await FindAsync(id, cancellationToken);

        task.MarkInProgress();
        await _repository.UpdateAsync(task, cancellationToken);

        _logger.LogInformation("Moved task '{Title}' to In Progress ({TaskId})", task.Title, task.Id);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await FindAsync(id, cancellationToken); // fail fast if the id is unknown
        await _repository.DeleteAsync(id, cancellationToken);

        _logger.LogInformation("Deleted task with id {TaskId}", id);
    }

    private async Task<TaskItem> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);

        return task ?? throw new TaskNotFoundException(id);
    }

    // Entities and DTOs are two different shapes on purpose; mapping is manual
    // and explicit so it is easy to follow (no magic auto-mapping).
    private static TaskDto ToDto(TaskItem task) =>
        new(
            task.Id,
            task.Title,
            task.Description,
            task.Priority,
            task.Status,
            task.DueDate,
            task.CreatedAt,
            task.UpdatedAt);
}
