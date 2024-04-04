using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaAn.Application.Interfaces;

namespace SaAn.Infrastructure.Tests;

[TestFixture]
[Category("Unit")]
internal class Test002Database
{
    private readonly ServiceProvider _serviceProvider;

    public Test002Database()
    {
        var services = new ServiceCollection();

        services.AddInfrastructure(
            "Server=localhost;Port=54322;Database=saan;Username=saan_user;Password='saan';IncludeErrorDetail=true");

        _serviceProvider = services.BuildServiceProvider(true);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _serviceProvider.Dispose();
    }

    [Test]
    public void Test001_ApplyMigrations()
    {
        using IServiceScope serviceScope = _serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IDbContext>();

        dbContext.Database.Migrate();

        dbContext.Database.CanConnect().Should().BeTrue();
    }
}