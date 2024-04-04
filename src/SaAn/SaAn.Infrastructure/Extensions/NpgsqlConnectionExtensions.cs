using Npgsql;
using SaAn.Domain.Enums;

namespace SaAn.Infrastructure.Extensions;

public static class NpgsqlConnectionExtensions
{
    public static NpgsqlDataSourceBuilder MapEnums(this NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<VehicleType>();

        return builder;
    }
}