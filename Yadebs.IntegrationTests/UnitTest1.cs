using Npgsql;
using System.Data.Common;
using Testcontainers.PostgreSql;

namespace Yadebs.IntegrationTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task TestMethod1Async()
    {
        PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();
        await _postgreSqlContainer.StartAsync();
        await using DbConnection connection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await using DbCommand command = new NpgsqlCommand();

        await connection.OpenAsync();
        command.Connection = connection;
        command.CommandText = "SELECT 1";
    }
}