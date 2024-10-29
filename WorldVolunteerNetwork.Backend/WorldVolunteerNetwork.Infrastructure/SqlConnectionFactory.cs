using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace WorldVolunteerNetwork.Infrastructure;

public class SqlConnectionFactory
{
    private readonly IConfiguration _configuration;
    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString("WorldVolunteerNetworkDbContext"));
    }
}
