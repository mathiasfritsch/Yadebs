using Npgsql;
using System.Data.Common;
using System.Net.Http.Json;
using Testcontainers.PostgreSql;
using Yadebs.Models.Dto;

namespace Yadebs.IntegrationTests;

[TestClass]
public class AccountsIntegrationTests
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


    [TestMethod]
    public async Task GetAccounts_Returns_Accounts()
    {
        var application = new WebApplicationSut();
        var client = application.CreateClient();
        var uri = new Uri($"{client.BaseAddress}api/Accounts");

        var accounts = await client.GetFromJsonAsync<AccountDto[]>(uri);

        Assert.IsTrue(accounts!.Length == 2);
    }
}