using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SaAn.Infrastructure;

public class ApplyMigrations : IHostedService
{
    private readonly ILogger<ApplyMigrations> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ApplyMigrations(IServiceScopeFactory serviceScopeFactory, ILogger<ApplyMigrations> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var migrationNames = (await dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();
        if (migrationNames.Count == 0)
        {
            _logger.LogInformation("No database migrations to apply!");
            return;
        }

        _logger.LogInformation("The following database migrations are pending {@pendingMigrationNames}.",
            migrationNames);

        await dbContext.Database.MigrateAsync(cancellationToken);

        _logger.LogInformation("All {pendingMigrationCount} pending database migrations have been applied!",
            migrationNames.Count);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopped.");
        return Task.CompletedTask;
    }
}