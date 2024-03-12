using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using System.Net.Http.Json;
using Testcontainers.PostgreSql;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.IntegrationTests;

[TestClass]
public class AccountsIntegrationTests
{
    private string _connectionString;
    private AccountingContext _accountingContext;

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.2")
        .WithDatabase("db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithCleanUp(true)
        .Build();

    [TestInitialize]
    public async Task TestInitialize()
    {

        await _postgreSqlContainer.StartAsync();

        _connectionString = _postgreSqlContainer.GetConnectionString();
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        await _postgreSqlContainer.DisposeAsync();
    }

    [TestMethod]
    public async Task GetAccounts_Returns_Accounts()
    {
        var application = new WebApplicationSut()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<AccountingContext>));
                    services.AddDbContext<AccountingContext>(options =>
                        options.UseNpgsql(
                            _connectionString,
                            o => o.UseNodaTime()
                        )
                    );
                });
            });

        var scope = application.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        _accountingContext = scope.ServiceProvider.GetRequiredService<AccountingContext>();
        _accountingContext.Database.EnsureCreated();

        var client = application.CreateClient();

        var book1 = new Book { Name = "Book1" };
        var account1 = new Account { Name = "Account1", Book = book1, Number = 568 };

        _accountingContext.Books.Add(book1);
        _accountingContext.Accounts.Add(account1);
        await _accountingContext.SaveChangesAsync();

        var uri = new Uri($"{client.BaseAddress}api/Accounts");
        var accounts = await client.GetFromJsonAsync<AccountDto[]>(uri);

        Assert.IsTrue(accounts!.Length == 1);

        Assert.IsTrue(accounts[0].Name == account1.Name);
        Assert.IsTrue(accounts[0].Number == account1.Number);

    }

}