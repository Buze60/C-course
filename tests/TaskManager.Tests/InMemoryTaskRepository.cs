using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests;

/// <summary>
/// A hand-written FAKE used only in unit tests. It stores tasks in memory so
/// TaskService can be tested without touching the file system. This is a core
/// benefit of depending on an interface instead of a concrete class.
/// </summary>
internal sealed class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = [];

    public Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(_tasks.ToList());

    public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_tasks.FirstOrDefault(task => task.Id == id));

    public Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var index = _tasks.FindIndex(existing => existing.Id == task.Id);
        if (index >= 0)
        {
            _tasks[index] = task;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _tasks.RemoveAll(task => task.Id == id);
        return Task.CompletedTask;
    }
}
