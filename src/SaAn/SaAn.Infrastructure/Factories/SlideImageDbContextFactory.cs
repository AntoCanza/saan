using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using SaAn.Infrastructure.Extensions;

namespace SaAn.Infrastructure.Factories;

public class SlideImageDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        if (args.Contains("--skip-connection"))
        {
            // we need to set a dummy connection string, otherwise we cannot build the data source.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=dummy");
            dataSourceBuilder.MapEnums();

            optionsBuilder.UseNpgsql(dataSourceBuilder.Build());
        }
        else
        {
            Console.WriteLine(
                "We assume you want to connect to a database as --skip-connection was not provided as argument.");

            string postgreSqlConfig = Environment.GetEnvironmentVariables().ToPostgreSqlConfig()
                .ParametersToConnectionString();
            optionsBuilder.UseNpgsql(postgreSqlConfig);
            Console.WriteLine(postgreSqlConfig);
        }

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}