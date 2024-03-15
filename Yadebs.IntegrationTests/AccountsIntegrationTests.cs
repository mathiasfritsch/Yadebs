using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
    private HttpClient _client;
    private Book _book1;
    private Account _account1;


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

        _book1 = new Book { Name = "Book1" };
        _account1 = new Account { Name = "Account1", Book = _book1, Number = 568 };

        _accountingContext.Books.Add(_book1);
        _accountingContext.Accounts.Add(_account1);
        await _accountingContext.SaveChangesAsync();

        _client = application.CreateClient();
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        await _postgreSqlContainer.DisposeAsync();
    }


    [TestMethod]
    public async Task GetAccounts_Returns_Accounts()
    {
        var uri = new Uri($"{_client.BaseAddress}api/Accounts");
        var accounts = await _client.GetFromJsonAsync<AccountDto[]>(uri);

        Assert.IsTrue(accounts!.Length == 1);

        Assert.IsTrue(accounts[0].Name == _account1.Name);
        Assert.IsTrue(accounts[0].Number == _account1.Number);
    }

    [TestMethod]
    public async Task UpdateAccount_Updates_Account()
    {
        var uri = new Uri($"{_client.BaseAddress}api/Accounts/1");
        await _client.PutAsJsonAsync(uri, new AccountUpdateDto { Id = 1, Name = "NewName", Number = 222 });

        var result = await _client.GetFromJsonAsync<AccountDto>(uri);

        // Assert.AreEqual(222, result.Number);
        // Assert.AreEqual("NewName", result.Name);
    }

    [TestMethod]
    public async Task AddAccount_Adds_Account()
    {
        var uri = new Uri($"{_client.BaseAddress}api/Accounts");
        await _client.PostAsJsonAsync(uri, new AccountAddDto { Name = "NewName", BookId = 1, IncreasesDebitWhenMoneyAdded = false, Number = 115 });


        var uriGetAll = new Uri($"{_client.BaseAddress}api/Accounts");
        var accounts = await _client.GetFromJsonAsync<AccountDto[]>(uriGetAll);

        Assert.IsTrue(accounts!.Length == 2);

    }

}