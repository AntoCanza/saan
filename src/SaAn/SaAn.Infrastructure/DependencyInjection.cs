using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SaAn.Application.Interfaces;
using SaAn.Infrastructure.Extensions;
using SaAn.Infrastructure.Services;

namespace SaAn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        string dbContextAssemblyName = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
        NpgsqlDataSource source = new NpgsqlDataSourceBuilder(connectionString).MapEnums().Build();

        services.AddDbContext<ApplicationDbContext>(dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(source,
                npgsqlDbContextOptionsBuilder =>
                    npgsqlDbContextOptionsBuilder.MigrationsAssembly(dbContextAssemblyName));
        });
        services.AddHostedService<ApplyMigrations>();
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetService<ApplicationDbContext>());
        services.AddScoped<VehicleService>();

        return services;
    }
}