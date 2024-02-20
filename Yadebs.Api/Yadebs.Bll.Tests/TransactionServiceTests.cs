using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Repository;
using Yadebs.Bll.Services;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Tests;

[TestClass]
public class TransactionServiceTests
{

    private TransactionService _transactionService = null!;
    private AccountingContext _accountingContext = null!;
    private Repository<Journal, JournalDto, JournalUpdateDto, JournalAddDto> _journalRepository = null!;

    [TestInitialize]
    public void Init()
    {
        _accountingContext = InMemoryContext.GetContext();
        _journalRepository = new Repository<Journal, JournalDto, JournalUpdateDto, JournalAddDto>(_accountingContext);
        _transactionService = new TransactionService(_accountingContext, _journalRepository);
        MapsterConfig.ConfigureMapster();
    }


    [TestMethod]
    public async Task GetJournalsAsyncGetsJournals()
    {
        _accountingContext.Add(
            new Journal
            {
                Id = 13,
                Description = "SomeDescription1",
                Date = new DateTime(2020, 10, 15)
            }
        );

        _accountingContext.Add(
            new Journal
            {
                Id = 59,
                Description = "SomeDescription2",
                Date = new DateTime(2020, 5, 20)
            }
        );


        await _accountingContext.SaveChangesAsync();

        var journals = await _transactionService.GetJournalsAsync();

        Assert.AreEqual(2, journals.Count());
    }
    [TestMethod]
    public async Task UpdateJournalAsyncUpdatesJournal()
    {
        CreateJournal(out var journalInStore);

        await _accountingContext.SaveChangesAsync();

        var journalFromStore = await _transactionService.GetJournalAsync(journalInStore.Id);

        var journalUpdate = journalFromStore.Adapt<JournalUpdateDto>();

        journalUpdate.Description = "Updated Description";
        journalUpdate.Date = new DateTime(2020, 4, 13);

        journalUpdate.Transactions[0].AccountId = 4235;
        journalUpdate.Transactions[0].Amount = (decimal)56.34;

        journalUpdate.Transactions[1].AccountId = 343;
        journalUpdate.Transactions[1].Amount = (decimal)321.78;

        var journalUpdated = await _transactionService.UpdateJournalAsync(journalInStore.Id, journalUpdate);

        Assert.AreEqual(journalUpdate.Id, journalUpdated.Id);
        Assert.AreEqual(journalUpdate.Description, journalUpdated.Description);
        Assert.AreEqual(journalUpdate.Date, journalUpdated.Date);

        Assert.AreEqual(2, journalUpdated.Transactions.Length);

        Assert.AreEqual(journalUpdate.Transactions[0].AccountId, journalUpdated.Transactions[0].AccountId);
        Assert.AreEqual(journalUpdate.Transactions[0].Amount, journalUpdated.Transactions[0].Amount);

        Assert.AreEqual(journalUpdate.Transactions[1].AccountId, journalUpdated.Transactions[1].AccountId);
        Assert.AreEqual(journalUpdate.Transactions[1].Amount, journalUpdated.Transactions[1].Amount);

    }
    [TestMethod]
    public async Task GetJournalAsyncGetsJournal()
    {
        CreateJournal(out var journalInStore);

        await _accountingContext.SaveChangesAsync();

        var journal = await _transactionService.GetJournalAsync(journalInStore.Id);

        Assert.AreEqual(journalInStore.Description, journal.Description);
        Assert.AreEqual(journalInStore.Date, journal.Date);
        Assert.AreEqual(journalInStore.Id, journal.Id);

        Assert.AreEqual(2, journal.Transactions.Length);

        Assert.AreEqual(journalInStore.Transactions[0].AccountId, journal.Transactions[0].AccountId);
        Assert.AreEqual(journalInStore.Transactions[0].Amount, journal.Transactions[0].Amount);
        Assert.AreEqual(journalInStore.Transactions[1].AccountId, journal.Transactions[1].AccountId);
        Assert.AreEqual(journalInStore.Transactions[1].Amount, journal.Transactions[1].Amount);
    }

    private void CreateJournal(out Journal journalInStore)
    {
        var accountInStore1 = new Account
        {
            Id = 254,
            Number = 658,
            Name = "Account1",
            IncreasesDebitWhenMoneyAdded = true,
        };
        var accountInStore2 = new Account
        {
            Id = 98,
            Number = 235,
            Name = "Account2",
            IncreasesDebitWhenMoneyAdded = false
        };
        var transactionInStore1 = new Transaction
        {
            Id = 458,
            AccountId = accountInStore1.Id,
            Amount = (decimal)205.25,
            Account = accountInStore1
        };
        var transactionInStore2 = new Transaction
        {
            Id = 987,
            AccountId = accountInStore2.Id,
            Amount = (decimal)587.15,
            Account = accountInStore2
        };

        journalInStore = new Journal
        {
            Id = 13,
            Description = "SomeDescription1",
            Date = new DateTime(2020, 10, 15),
            Transactions = new List<Transaction>()
        };
        journalInStore.Transactions.Add(transactionInStore1);
        journalInStore.Transactions.Add(transactionInStore2);

        _accountingContext.AddRange(journalInStore);
    }

    [TestMethod]
    public async Task DeleteJournalsAsyncDeletesJournal()
    {
        var journalInStoreToDelete = new Journal
        {
            Id = 13,
            Description = "SomeDescription1",
            Date = new DateTime(2020, 10, 15)
        };
        var journalInStoreToKeep = new Journal
        {
            Id = 234,
            Description = "SomeDescription2",
            Date = new DateTime(2022, 4, 11)
        };

        await _accountingContext.AddRangeAsync(journalInStoreToDelete, journalInStoreToKeep);

        await _accountingContext.SaveChangesAsync();

        await _transactionService.DeleteJournalAsync(journalInStoreToDelete.Id);

        var journalsAfterDelete = await _accountingContext.Journals.ToListAsync();

        Assert.AreEqual(0, journalsAfterDelete.Count(j => j.Id == journalInStoreToDelete.Id));
        Assert.AreEqual(1, journalsAfterDelete.Count(j => j.Id == journalInStoreToKeep.Id));
    }

    [TestMethod]
    public async Task AddJournalAsyncAddsJournal()
    {
        var accountInStore1 = new Account
        {
            Id = 254,
            Number = 658,
            Name = "Account1",
            IncreasesDebitWhenMoneyAdded = true
        };
        var accountInStore2 = new Account
        {
            Id = 98,
            Number = 235,
            Name = "Account2",
            IncreasesDebitWhenMoneyAdded = false
        };

        await _accountingContext.AddRangeAsync(accountInStore1, accountInStore2);
        await _accountingContext.SaveChangesAsync();

        var journalToAdd = new JournalAddDto
        {
            Description = "SomeDescription1",
            Date = new DateTime(2020, 10, 15),
            Transactions = new[]
            {
                new TransactionAddDto()
                {
                    Amount = (decimal)205.15,
                    AccountId = accountInStore1.Id,
                },
                new TransactionAddDto()
                {
                    Amount = (decimal)698.50,
                    AccountId = accountInStore2.Id,
                }
            }
        };

        var journalAdded = await _transactionService.AddJournalAsync(journalToAdd);

        await _accountingContext.Journals.ToListAsync();

        var journalsAfterAdd = await _accountingContext.Journals
            .Include(j => j.Transactions)
            .ThenInclude(t => t.Account)
            .SingleAsync(j => j.Id == journalAdded.Id);

        Assert.AreEqual(journalToAdd.Description, journalsAfterAdd.Description);
        Assert.AreEqual(journalToAdd.Date, journalsAfterAdd.Date);
        Assert.AreEqual(2, journalsAfterAdd.Transactions.Count());

        Assert.AreEqual(journalToAdd.Transactions[0].Amount, journalsAfterAdd.Transactions[0].Amount);
        Assert.AreEqual(journalToAdd.Transactions[0].AccountId, journalsAfterAdd.Transactions[0].AccountId);


        Assert.AreEqual(journalToAdd.Transactions[1].Amount, journalsAfterAdd.Transactions[1].Amount);
        Assert.AreEqual(journalToAdd.Transactions[1].AccountId, journalsAfterAdd.Transactions[1].AccountId);
    }
}