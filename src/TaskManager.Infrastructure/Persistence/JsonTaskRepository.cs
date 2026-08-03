using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

/// <summary>
/// Concrete implementation of <see cref="ITaskRepository"/> that persists
/// tasks to a single JSON file.
///
/// This is the Infrastructure layer — it knows the ugly details (file system,
/// serialization) so the rest of the app does not have to.
/// </summary>
public sealed class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private readonly ILogger<JsonTaskRepository> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonTaskRepository(string filePath, ILogger<JsonTaskRepository> logger)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        _filePath = filePath;
        _logger = logger;

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }, // store "High", not "3"
        };
    }

    public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        await using var stream = File.OpenRead(_filePath);
        var tasks = await JsonSerializer.DeserializeAsync<List<TaskItem>>(stream, _jsonOptions, cancellationToken);

        return tasks ?? [];
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var tasks = await GetAllAsync(cancellationToken);
        return tasks.FirstOrDefault(task => task.Id == id);
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var tasks = await GetAllAsync(cancellationToken);

        tasks.Add(task);
        await SaveAsync(tasks, cancellationToken);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var tasks = await GetAllAsync(cancellationToken);
        var index = tasks.FindIndex(existing => existing.Id == task.Id);

        if (index < 0)
        {
            _logger.LogWarning("Tried to update a task that no longer exists ({TaskId})", task.Id);
            return;
        }

        tasks[index] = task;
        await SaveAsync(tasks, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var tasks = await GetAllAsync(cancellationToken);

        tasks.RemoveAll(task => task.Id == id);
        await SaveAsync(tasks, cancellationToken);
    }

    private async Task SaveAsync(List<TaskItem> tasks, CancellationToken cancellationToken)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, tasks, _jsonOptions, cancellationToken);
    }
}
