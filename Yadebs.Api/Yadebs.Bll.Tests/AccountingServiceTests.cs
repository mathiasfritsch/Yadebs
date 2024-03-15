using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Repository;
using Yadebs.Bll.Services;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Tests;

[TestClass]
public class AccountingServiceTests
{
    private AccountingService _accountingService = null!;
    private AccountingContext _accountingContext = null!;
    private Repository<Account, AccountDto, AccountUpdateDto, AccountAddDto> _accountingRepository = null!;

    [TestInitialize]
    public void Init()
    {
        _accountingContext = InMemoryContext.GetContext();
        _accountingRepository = new Repository<Account, AccountDto, AccountUpdateDto, AccountAddDto>(_accountingContext);
        _accountingService = new AccountingService(_accountingContext, _accountingRepository);
    }

    [TestMethod]
    public async Task GetAccountsGetsResults_Test()
    {
        _accountingContext.Add(
            new Account
            {
                Id = 13,
                BookId = 1,
                Name = "SomeAccount1",
                Number = 123445
            }
        );
        _accountingContext.Add(
            new Account
            {
                Id = 21,
                BookId = 1,
                Name = "SomeAccount2",
                Number = 6587,
                ParentId = 13
            }
        );
        await _accountingContext.SaveChangesAsync();

        var accounts = await _accountingService.GetAccountsAsync();

        Assert.AreEqual(2, accounts.Count());
    }

    [TestMethod]
    public async Task GetAccountGetsResult_Test()
    {
        _accountingContext.Add(
            new Account
            {
                Id = 13,
                BookId = 1,
                Name = "SomeAccount1",
                Number = 123445
            }
        );

        await _accountingContext.SaveChangesAsync();

        var account = await _accountingService.GetAccountAsync(13);

        Assert.AreEqual(13, account.Id);
        Assert.AreEqual(1, account.BookId);
        Assert.AreEqual("SomeAccount1", account.Name);
        Assert.AreEqual(123445, account.Number);
    }

    [TestMethod]
    public async Task AddAccountAddsAccount_Test()
    {
        var accountAdd = new AccountAddDto()
        {
            BookId = 1,
            Name = "SomeAccount1",
            Number = 123445,
            ParentId = 256
        };

        var accountAdded = await _accountingService.AddAccountAsync(accountAdd);

        var accountReload = await _accountingService.GetAccountAsync(accountAdded.Id);

        Assert.AreEqual(accountAdded.Id, accountReload.Id);
        Assert.AreEqual(accountAdd.BookId, accountReload.BookId);
        Assert.AreEqual(accountAdd.Name, accountReload.Name);
        Assert.AreEqual(accountAdd.Number, accountReload.Number);
        Assert.AreEqual(accountAdd.ParentId, accountReload.ParentId);

    }

    [TestMethod]
    public async Task UpdateAccountUpdatesAccount_Test()
    {
        var account = new Account
        {
            Id = 13,
            BookId = 1,
            Name = "SomeAccount1",
            Number = 123445
        };

        _accountingContext.Add(account);

        await _accountingContext.SaveChangesAsync();

        var accountToUpdate = (await _accountingService.GetAccountAsync(account.Id)).Adapt<AccountUpdateDto>();

        accountToUpdate.ParentId = 58;
        accountToUpdate.BookId = 2;
        accountToUpdate.Number = 54987;
        accountToUpdate.Name = "SomeUpdatedAccount";

        await _accountingService.UpdateAccountAsync(account.Id, accountToUpdate);

        var accountReload = await _accountingService.GetAccountAsync(account.Id);

        Assert.AreEqual(accountToUpdate.Id, accountReload.Id);
        Assert.AreEqual(accountToUpdate.BookId, accountReload.BookId);
        Assert.AreEqual(accountToUpdate.Name, accountReload.Name);
        Assert.AreEqual(accountToUpdate.Number, accountReload.Number);
    }

    [TestMethod]
    public async Task DeleteAccountDeletesAccount_Test()
    {
        var accountToStay = new Account
        {
            Id = 13,
            BookId = 1,
            Name = "SomeAccount1",
            Number = 123445
        };
        var accountToDelete = new Account
        {
            Id = 21,
            BookId = 1,
            Name = "SomeAccount2",
            Number = 6587,
            ParentId = 13
        };

        _accountingContext.AddRange(accountToStay, accountToDelete);

        await _accountingContext.SaveChangesAsync();

        await _accountingService.DeleteAccountAsync(accountToDelete.Id);

        var accountsInStore = await _accountingContext.Accounts.ToListAsync();

        Assert.AreEqual(0, accountsInStore.Count(a => a.Id == accountToDelete.Id));
        Assert.AreEqual(1, accountsInStore.Count(a => a.Id == accountToStay.Id));
    }
}