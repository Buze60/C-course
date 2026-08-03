using Microsoft.Extensions.Logging.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Tests;

/// <summary>
/// Tests for the JSON file persistence. Each test writes to its own unique
/// temporary file and removes it afterwards, so tests never interfere.
/// </summary>
public class JsonTaskRepositoryTests
{
    private readonly string _tempDirectory;
    private readonly string _filePath;

    public JsonTaskRepositoryTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "TaskManagerTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDirectory);
        _filePath = Path.Combine(_tempDirectory, "tasks.json");
    }

    [Fact]
    public async Task GetAllAsync_FileDoesNotExist_ReturnsEmptyList()
    {
        var repository = CreateRepository();

        var tasks = await repository.GetAllAsync();

        Assert.Empty(tasks);
    }

    [Fact]
    public async Task AddThenGetById_ReturnsSavedTask()
    {
        var repository = CreateRepository();
        var task = TaskItem.Create("Write test", "unit tests", TaskPriority.High, null);

        await repository.AddAsync(task);

        var loaded = await repository.GetByIdAsync(task.Id);
        Assert.NotNull(loaded);
        Assert.Equal("Write test", loaded!.Title);
        Assert.Equal(TaskPriority.High, loaded.Priority);
        Assert.Equal(TaskState.Pending, loaded.Status);
    }

    [Fact]
    public async Task Data_SurvivesRestart_OfTheApplication()
    {
        var task = TaskItem.Create("Persist me", null!, TaskPriority.Medium, new DateTime(2026, 9, 1));

        // First "run" of the app writes the file...
        var firstRepository = CreateRepository();
        await firstRepository.AddAsync(task);

        // Second "run" creates a brand-new repository over the same file.
        var secondRepository = CreateRepository();
        var tasks = await secondRepository.GetAllAsync();

        var saved = Assert.Single(tasks);
        Assert.Equal(task.Id, saved.Id);
        Assert.Equal("Persist me", saved.Title);
        Assert.Equal(new DateTime(2026, 9, 1), saved.DueDate);
        Assert.Equal(TaskState.Pending, saved.Status);
    }

    [Fact]
    public async Task UpdateAsync_ChangesSavedValues()
    {
        var repository = CreateRepository();
        var task = TaskItem.Create("Before", null!, TaskPriority.Low, null);
        await repository.AddAsync(task);

        task.Update("After", "changed", TaskPriority.High, null);
        await repository.UpdateAsync(task);

        var loaded = await repository.GetByIdAsync(task.Id);
        Assert.Equal("After", loaded!.Title);
        Assert.Equal(TaskPriority.High, loaded.Priority);
    }

    [Fact]
    public async Task DeleteAsync_RemovesTaskFromDisk()
    {
        var repository = CreateRepository();
        var task = TaskItem.Create("Gone soon", null!, TaskPriority.Low, null);
        await repository.AddAsync(task);

        await repository.DeleteAsync(task.Id);

        Assert.Null(await repository.GetByIdAsync(task.Id));
        Assert.Empty(await repository.GetAllAsync());
    }

    private JsonTaskRepository CreateRepository()
        => new(_filePath, NullLogger<JsonTaskRepository>.Instance);
}
