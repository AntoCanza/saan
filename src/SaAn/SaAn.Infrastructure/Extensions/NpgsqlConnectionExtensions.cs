using Npgsql;

namespace SaAn.Infrastructure.Extensions;

public static class NpgsqlConnectionExtensions
{
    public static NpgsqlDataSourceBuilder MapEnums(this NpgsqlDataSourceBuilder builder)
    {
        //builder.MapEnum<RtcCredentialType>();

        return builder;
    }
}