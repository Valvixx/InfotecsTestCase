using Infrastructure.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Dapper;

public class DapperSettings : IDapperSettings
{
    public DapperSettings(IConfiguration configuration)
    {
        ConnectionString = configuration["Connections:Database"] ??
                           throw new ArgumentException("Set database connection string");
    }

    public string ConnectionString { get; }
}