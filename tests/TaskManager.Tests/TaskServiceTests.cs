using Microsoft.Extensions.Logging.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Services;
using TaskManager.Domain.Enums;

namespace TaskManager.Tests;

/// <summary>
/// Unit tests for the business logic in <see cref="TaskService"/>.
/// Notice the service is constructed with the in-memory fake repository and a
/// no-op logger — no file system, no I/O, so the tests are fast and reliable.
/// </summary>
public class TaskServiceTests
{
    private readonly InMemoryTaskRepository _repository;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        _repository = new InMemoryTaskRepository();
        _service = new TaskService(_repository, NullLogger<TaskService>.Instance);
    }

    [Fact]
    public async Task CreateAsync_CreatesPendingTask()
    {
        var dto = new CreateTaskDto("  Buy milk  ", "1 liter", TaskPriority.High, null);

        var result = await _service.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Buy milk", result.Title);      // title is trimmed
        Assert.Equal(TaskState.Pending, result.Status);
        Assert.Equal(TaskPriority.High, result.Priority);
    }

    [Fact]
    public async Task CreateAsync_EmptyTitle_ThrowsInvalidTaskException()
    {
        var dto = new CreateTaskDto("   ", null!, TaskPriority.Low, null);

        var exception = await Assert.ThrowsAsync<InvalidTaskException>(() => _service.CreateAsync(dto));

        Assert.Contains("empty", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(await _repository.GetAllAsync());
    }

    [Fact]
    public async Task CompleteAsync_MarksTaskAsCompleted()
    {
        var created = await _service.CreateAsync(new CreateTaskDto("Write code", null!, TaskPriority.Medium, null));

        await _service.CompleteAsync(created.Id);

        var updated = await _service.GetByIdAsync(created.Id);
        Assert.Equal(TaskState.Completed, updated.Status);
    }

    [Fact]
    public async Task CompleteAsync_UnknownId_ThrowsTaskNotFoundException()
    {
        await Assert.ThrowsAsync<TaskNotFoundException>(() => _service.CompleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task MarkInProgressAsync_MovesTaskToInProgress()
    {
        var created = await _service.CreateAsync(new CreateTaskDto("Learn C#", null!, TaskPriority.Low, null));

        await _service.MarkInProgressAsync(created.Id);

        var updated = await _service.GetByIdAsync(created.Id);
        Assert.Equal(TaskState.InProgress, updated.Status);
    }

    [Fact]
    public async Task UpdateAsync_ChangesFields()
    {
        var created = await _service.CreateAsync(new CreateTaskDto("Old title", "old desc", TaskPriority.Low, null));
        var dto = new UpdateTaskDto("New title", "new desc", TaskPriority.High, new DateTime(2026, 12, 31));

        var updated = await _service.UpdateAsync(created.Id, dto);

        Assert.Equal("New title", updated.Title);
        Assert.Equal("new desc", updated.Description);
        Assert.Equal(TaskPriority.High, updated.Priority);
        Assert.Equal(new DateTime(2026, 12, 31), updated.DueDate);
    }

    [Fact]
    public async Task UpdateAsync_EmptyTitle_ThrowsInvalidTaskException()
    {
        var created = await _service.CreateAsync(new CreateTaskDto("Keep me", null!, TaskPriority.Low, null));
        var dto = new UpdateTaskDto("", null!, TaskPriority.Low, null);

        await Assert.ThrowsAsync<InvalidTaskException>(() => _service.UpdateAsync(created.Id, dto));
    }

    [Fact]
    public async Task DeleteAsync_RemovesTask()
    {
        var created = await _service.CreateAsync(new CreateTaskDto("Remove me", null!, TaskPriority.Medium, null));

        await _service.DeleteAsync(created.Id);

        Assert.Empty(await _service.GetAllAsync());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsTasksNewestFirst()
    {
        await _service.CreateAsync(new CreateTaskDto("First", null!, TaskPriority.Low, null));
        await _service.CreateAsync(new CreateTaskDto("Second", null!, TaskPriority.Low, null));

        var tasks = await _service.GetAllAsync();

        Assert.Equal(2, tasks.Count);
        Assert.Equal("Second", tasks[0].Title);
        Assert.Equal("First", tasks[1].Title);
    }
}
