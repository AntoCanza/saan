using System.Text.Json.Serialization;

namespace SaAn.Application.Config;

public class PostgreSqlConfig
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Database { get; set; }

    public string Username { get; set; }

    [JsonIgnore] public string Password { get; set; }

    public string ParametersToConnectionString()
    {
        return
            $"Server={Host};Port={Port};Database={Database};Username={Username};Password='{Password}';IncludeErrorDetail=true";
    }

    public override string ToString()
    {
        return $"Server={Host};Port={Port};Database={Database};Username={Username};Password=********";
    }
}