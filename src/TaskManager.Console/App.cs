using Microsoft.Extensions.Logging;
using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Enums;

namespace TaskManager.Console;

/// <summary>
/// Presentation layer: knows how to talk to the user (menu, input, output).
/// It does NOT know about JSON files, repositories or entities — it only
/// depends on <see cref="ITaskService"/>.
/// </summary>
public sealed class App
{
    private readonly ITaskService _service;
    private readonly ILogger<App> _logger;

    public App(ITaskService service, ILogger<App> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        PrintHeader();

        while (true)
        {
            PrintMenu();
            var choice = ConsoleUi.ReadString("Option");

            try
            {
                switch (choice.ToLowerInvariant())
                {
                    case "1":
                    case "list":
                        await ListTasksAsync();
                        break;

                    case "2":
                    case "add":
                        await AddTaskAsync();
                        break;

                    case "3":
                    case "edit":
                        await EditTaskAsync();
                        break;

                    case "4":
                    case "complete":
                        await CompleteTaskAsync();
                        break;

                    case "5":
                    case "progress":
                        await MarkInProgressAsync();
                        break;

                    case "6":
                    case "delete":
                        await DeleteTaskAsync();
                        break;

                    case "0":
                    case "exit":
                    case "quit":
                        System.Console.WriteLine("\n  Goodbye!");
                        return;

                    default:
                        System.Console.WriteLine("  [!] Unknown option, try again.");
                        break;
                }
            }
            catch (InvalidTaskException ex)
            {
                System.Console.WriteLine($"  [!] {ex.Message}");
            }
            catch (TaskNotFoundException ex)
            {
                System.Console.WriteLine($"  [!] {ex.Message}");
            }
            catch (Exception ex)
            {
                // Anything unexpected: log the details, keep the app alive.
                _logger.LogError(ex, "Unexpected error while running command '{Choice}'", choice);
                System.Console.WriteLine("  [!] Something went wrong. See the log output above.");
            }

            System.Console.WriteLine();
        }
    }

    private async Task ListTasksAsync()
    {
        var tasks = await _service.GetAllAsync();

        if (tasks.Count == 0)
        {
            System.Console.WriteLine("  (no tasks yet — choose option 2 to add one)");
            return;
        }

        System.Console.WriteLine();
        foreach (var task in tasks)
        {
            PrintTask(task);
        }
    }

    private static void PrintTask(TaskDto task)
    {
        string statusIcon = task.Status switch
        {
            TaskState.Completed => "[x]",
            TaskState.InProgress => "[~]",
            _ => "[ ]",
        };

        var dueDate = task.DueDate.HasValue ? $"  due: {task.DueDate:yyyy-MM-dd}" : string.Empty;

        System.Console.WriteLine($"  {statusIcon} {task.Title}");
        System.Console.WriteLine($"      id:         {task.Id}");
        System.Console.WriteLine($"      priority:   {task.Priority}");
        System.Console.WriteLine($"      status:     {task.Status}{dueDate}");

        if (!string.IsNullOrWhiteSpace(task.Description))
        {
            System.Console.WriteLine($"      notes:      {task.Description}");
        }
    }

    private async Task AddTaskAsync()
    {
        System.Console.WriteLine();
        var title = ConsoleUi.ReadRequiredString("Title");
        var description = ConsoleUi.ReadString("Description (optional)");
        var priority = ConsoleUi.ReadEnum<TaskPriority>("Priority");
        var dueDate = ConsoleUi.ReadOptionalDate("Due date (optional)");

        var dto = new CreateTaskDto(title, description, priority, dueDate);
        var created = await _service.CreateAsync(dto);

        System.Console.WriteLine($"  [+] Created task '{created.Title}'");
    }

    private async Task EditTaskAsync()
    {
        var id = ConsoleUi.ReadGuid("Task id to edit");
        var existing = await _service.GetByIdAsync(id);

        System.Console.WriteLine();
        System.Console.WriteLine("  (press Enter to keep the current value)");

        var title = ConsoleUi.ReadOptional("Title", existing.Title);
        var description = ConsoleUi.ReadOptional("Description", existing.Description);
        var priority = ConsoleUi.ReadEnumOptional("Priority", existing.Priority);
        var dueDate = ConsoleUi.ReadOptionalDate("Due date", existing.DueDate);

        var dto = new UpdateTaskDto(title, description, priority, dueDate);
        await _service.UpdateAsync(id, dto);

        System.Console.WriteLine($"  [~] Task '{title}' updated");
    }

    private async Task CompleteTaskAsync()
    {
        var id = ConsoleUi.ReadGuid("Task id to complete");
        await _service.CompleteAsync(id);

        System.Console.WriteLine("  [x] Task marked as completed");
    }

    private async Task MarkInProgressAsync()
    {
        var id = ConsoleUi.ReadGuid("Task id to mark in progress");
        await _service.MarkInProgressAsync(id);

        System.Console.WriteLine("  [~] Task marked as in progress");
    }

    private async Task DeleteTaskAsync()
    {
        var id = ConsoleUi.ReadGuid("Task id to delete");
        var confirm = ConsoleUi.ReadString("Are you sure? Type 'y' to confirm");

        if (!confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            System.Console.WriteLine("  Cancelled.");
            return;
        }

        await _service.DeleteAsync(id);

        System.Console.WriteLine("  [-] Task deleted");
    }

    private static void PrintHeader()
    {
        System.Console.WriteLine("==============================================");
        System.Console.WriteLine("  Task Manager — a layered .NET console app");
        System.Console.WriteLine("==============================================");
    }

    private static void PrintMenu()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("  1. List all tasks");
        System.Console.WriteLine("  2. Add a task");
        System.Console.WriteLine("  3. Edit a task");
        System.Console.WriteLine("  4. Mark a task as completed");
        System.Console.WriteLine("  5. Mark a task as in progress");
        System.Console.WriteLine("  6. Delete a task");
        System.Console.WriteLine("  0. Exit");
        System.Console.WriteLine();
    }
}
