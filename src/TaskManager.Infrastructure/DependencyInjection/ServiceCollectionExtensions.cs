using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Abstractions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.DependencyInjection;

/// <summary>
/// Each layer exposes one "Add..." extension method that registers its own
/// services. The composition root (Program.cs) just calls them. This keeps
/// the dependency graph readable and centralised.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string jsonFilePath)
    {
        // Register the abstraction (ITaskRepository) against its concrete
        // implementation. Singleton because a console app runs one instance.
        services.AddSingleton<ITaskRepository>(provider =>
            new JsonTaskRepository(
                jsonFilePath,
                provider.GetRequiredService<ILogger<JsonTaskRepository>>()));

        return services;
    }
}
