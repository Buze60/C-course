# TaskManager

A beginner-friendly but **professionally structured** .NET 10 console application for managing tasks.

It is intentionally small, but it demonstrates the same architecture and practices used in
real-world .NET projects — so you can study every concept in one place.

---

## What it does

A menu-driven task manager. You can:

- List all tasks
- Add a task (title, optional description, priority, optional due date)
- Edit a task
- Mark a task as completed or in progress
- Delete a task

Tasks are saved to a `tasks.json` file and survive application restarts.

## How to run it

```bash
cd src/TaskManager.Console
dotnet run
```

Run the tests:

```bash
dotnet test TaskManager.slnx
```

Build the whole solution:

```bash
dotnet build TaskManager.slnx
```

## Project structure

```
TaskManager/
├── TaskManager.slnx              # solution file
├── Directory.Build.props         # shared build settings for all projects
├── .editorconfig                 # shared code style rules
├── src/
│   ├── TaskManager.Domain/        # business rules + entities (no dependencies)
│   ├── TaskManager.Application/   # use-cases, DTOs, interfaces, services
│   ├── TaskManager.Infrastructure/# storage details (JSON file persistence)
│   └── TaskManager.Console/       # presentation (menu, input, DI wiring)
└── tests/
    └── TaskManager.Tests/         # unit tests (xUnit)
```

## The architecture (what to learn here)

The code is split into **layers**, each with a single responsibility. Rules about dependencies:

```
Console  ──>  Application  ──>  Domain
   │              │
   └──────────────┴──────────>  Infrastructure
Infrastructure  ──>  Domain + Application (implements the interfaces)
```

| Concept | Where to look |
| --- | --- |
| **Layered / clean architecture** | `src/` has 4 projects; Domain has zero framework dependencies |
| **Entities + encapsulation** | `TaskManager.Domain/Entities/TaskItem.cs` — the entity protects itself (no empty titles, completed tasks can't go back to in progress) |
| **DTOs vs Entities** | `TaskManager.Application/Dtos` — records that move data between layers, never leak to the UI |
| **Repository pattern** | `Application/Abstractions/ITaskRepository.cs` + `Infrastructure/Persistence/JsonTaskRepository.cs` |
| **Dependency Inversion** | The Application layer depends on `ITaskRepository`, not on JSON/SQL. Swap the implementation in DI and nothing else changes |
| **Dependency Injection (DI)** | `Program.cs` (the Composition Root) + `Infrastructure/DependencyInjection/ServiceCollectionExtensions.cs` |
| **Logging** | `ILogger<T>` used in `TaskService` and the repository; console provider configured in `Program.cs` |
| **Configuration** | `appsettings.json` + `builder.Configuration` — the data store path is configurable |
| **Async/await** | every repository/service method is `async` with `CancellationToken` |
| **Error handling** | custom exceptions (`TaskNotFoundException`, `InvalidTaskException`) caught in the UI |
| **Nullable reference types** | `<Nullable>enable</Nullable>` everywhere; `string?` used correctly |
| **Unit testing** | `tests/` uses xUnit plus an in-memory fake repository so `TaskService` is tested without touching the file system |

## The SOLID principles in this project

- **S**ingle responsibility — every class does one thing (`TaskService` = business logic, `JsonTaskRepository` = storage, `App` = UI).
- **O**pen/closed — add SQL Server by creating a new `ITaskRepository` implementation; existing code doesn't change.
- **L**iskov substitution — `JsonTaskRepository` and the test fake both satisfy `ITaskRepository` and are interchangeable.
- **I**nterface segregation — small, focused interfaces (`ITaskRepository`, `ITaskService`).
- **D**ependency inversion — high-level modules (`TaskService`) depend on abstractions, not on low-level modules (JSON file).

## Common beginner questions

**Why is the project split into 4 projects?**
So that each layer can only depend on the layers it is allowed to depend on. This keeps the
codebase from turning into a tangled mess as it grows, and makes testing easier.

**Why does the entity have private setters?**
Encapsulation: only the entity can change its own state, through safe methods like
`Complete()`. Nobody outside can put the object into an invalid state.

**Why `TaskState` and not `TaskStatus`?**
`System.Threading.Tasks.TaskStatus` is a built-in .NET type. Naming ours `TaskStatus` would
cause a compile error in any file using both namespaces — a very common real-world gotcha.

**How does the JSON file get written?**
`JsonTaskRepository` is the concrete implementation of `ITaskRepository`. It uses
`System.Text.Json` to serialize the whole list to `tasks.json` after every change.
`[JsonConstructor]` + `[JsonInclude]` in `TaskItem` are there so the serializer can
recreate entities that have private setters.

**What is the Composition Root?**
`Program.cs`. It is the *only* place where all layers meet. It builds the DI container and
decides which implementation each interface uses — that is what makes the whole app
swappable and testable.

## Ideas to extend it (great practice!)

1. Add a "filter by status" option to the list command.
2. Store tasks in a SQLite database instead of JSON — write `SqliteTaskRepository : ITaskRepository`, then change one line in `Program.cs`.
3. Add a `DateOnly` due date filter and show overdue tasks in red.
4. Add validation: disallow a due date in the past (in the entity).
5. Use structured logging with a file or a service like Serilog.
