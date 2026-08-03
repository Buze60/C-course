using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Abstractions;
using TaskManager.Application.Services;
using TaskManager.Console;
using TaskManager.Infrastructure.DependencyInjection;

// ─────────────────────────────────────────────────────────────────────────────
//  COMPOSITION ROOT
//  This is the ONLY place in the whole app that knows about ALL the layers.
//  It builds the Dependency Injection container and decides which concrete
//  implementation every interface gets.
//
//  Swap JsonTaskRepository for SqlTaskRepository tomorrow? Change one line
//  in AddInfrastructure and you are done — no other code changes.
// ─────────────────────────────────────────────────────────────────────────────
var builder = Host.CreateApplicationBuilder(args);

// The generic host loads appsettings.json (and logging config) automatically.
var storePath = builder.Configuration["Data:TaskStorePath"]
    ?? Path.Combine(AppContext.BaseDirectory, "tasks.json");

builder.Logging.AddConsole();

builder.Services
    .AddInfrastructure(storePath)                        // ITaskRepository -> JsonTaskRepository
    .AddScoped<ITaskService, TaskService>()              // business service
    .AddScoped<App>();                                   // our menu-driven entry class

using var host = builder.Build();

try
{
    var app = host.Services.GetRequiredService<App>();
    await app.RunAsync();
}
catch (Exception ex)
{
    // Last-resort safety net: never let the app die with an ugly stack trace.
    var logger = host.Services.GetRequiredService<ILoggerFactory>()
        .CreateLogger("Program");

    logger.LogCritical(ex, "The application crashed");
}
