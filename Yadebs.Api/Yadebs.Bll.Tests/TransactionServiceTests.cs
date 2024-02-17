using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Services;
using Yadebs.Db;
using Yadebs.Models.Dto.Journal;

namespace Yadebs.Bll.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        [TestMethod]
        public async Task GetJournalsAsyncGetsJournals()
        {
            var context = InMemoryContext.GetContext();
            MapsterConfig.ConfigureMapster();
            context.Add(
                new Journal
                {
                    Id = 13,
                    Description = "SomeDescription1",
                    Date = new DateTime(2020, 10, 15)
                }
                );

            context.Add(
                new Journal
                {
                    Id = 59,
                    Description = "SomeDescription2",
                    Date = new DateTime(2020, 5, 20)
                }
                );

            await context.SaveChangesAsync();
            var transactionService = new TransactionService(context);

            var journals = await transactionService.GetJournalsAsync();

            Assert.AreEqual(2, journals.Count());
        }
        [TestMethod]
        public async Task UpdateJournalAsyncUpdatesJournal()
        {
            var context = InMemoryContext.GetContext();
            MapsterConfig.ConfigureMapster();

            Journal journalInStore;
            Account accountInStore1, accountInStore2;
            Transaction transactionInStore1, transactionInStore2;
            CreateJournal(
                context,
                out journalInStore,
                out accountInStore1,
                out accountInStore2,
                out transactionInStore1,
                out transactionInStore2);

            context.Add(journalInStore);

            await context.SaveChangesAsync();
            var transactionService = new TransactionService(context);


            var journalFromStoree = await transactionService.GetJournalAsync(journalInStore.Id);

            var journalUpdate = journalFromStoree.Adapt<JournalUpdateDto>();


            journalUpdate.Description = "Updated Description";
            journalUpdate.Date = new DateTime(2020, 4, 13);

            journalUpdate.Transactions[0].AccountId = 4235;
            journalUpdate.Transactions[0].Amount = (decimal)56.34;

            journalUpdate.Transactions[1].AccountId = 343;
            journalUpdate.Transactions[1].Amount = (decimal)321.78;

            var journalUpdated = await transactionService.UpdateJournalAsync(journalInStore.Id, journalUpdate);

            var trans = await context.Transactions.ToListAsync();

            Assert.AreEqual(journalUpdate.Id, journalUpdated.Id);
            Assert.AreEqual(journalUpdate.Description, journalUpdated.Description);
            Assert.AreEqual(journalUpdate.Date, journalUpdated.Date);

            Assert.AreEqual(2, journalUpdated.Transactions.Count());

            Assert.AreEqual(journalUpdate.Transactions[0].AccountId, journalUpdated.Transactions[0].AccountId);
            Assert.AreEqual(journalUpdate.Transactions[0].Amount, journalUpdated.Transactions[0].Amount);

            Assert.AreEqual(journalUpdate.Transactions[1].AccountId, journalUpdated.Transactions[1].AccountId);
            Assert.AreEqual(journalUpdate.Transactions[1].Amount, journalUpdated.Transactions[1].Amount);

        }
        [TestMethod]
        public async Task GetJournalAsyncGetsJournal()
        {
            var context = InMemoryContext.GetContext();
            MapsterConfig.ConfigureMapster();

            Journal journalInStore;
            Account accountInStore1, accountInStore2;
            Transaction transactionInStore1, transactionInStore2;
            CreateJournal(
                context,
                out journalInStore,
                out accountInStore1,
                out accountInStore2,
                out transactionInStore1,
                out transactionInStore2);

            context.Add(journalInStore);

            await context.SaveChangesAsync();
            var transactionService = new TransactionService(context);

            var journal = await transactionService.GetJournalAsync(journalInStore.Id);

            Assert.AreEqual(journalInStore.Id, journal.Id);
            Assert.AreEqual(journalInStore.Description, journal.Description);
            Assert.AreEqual(journalInStore.Date, journal.Date);
            Assert.AreEqual(journalInStore.Id, journal.Id);

            Assert.AreEqual(2, journal.Transactions.Count());

            Assert.AreEqual(accountInStore1.Id, journal.Transactions[0].AccountId);
            Assert.AreEqual(transactionInStore1.Amount, journal.Transactions[0].Amount);

            Assert.AreEqual(accountInStore2.Id, journal.Transactions[1].AccountId);
            Assert.AreEqual(transactionInStore2.Amount, journal.Transactions[1].Amount);
        }

        private static void CreateJournal(
            AccountingContext context,
            out Journal journalInStore,
            out Account accountInStore1,
            out Account accountInStore2,
            out Transaction transactionInStore1,
            out Transaction transactionInStore2)
        {
            journalInStore = new Journal
            {
                Id = 13,
                Description = "SomeDescription1",
                Date = new DateTime(2020, 10, 15)
            };
            accountInStore1 = new Account
            {
                Id = 254,
                Number = 658,
                Name = "Account1",
                IncreasesDebitWhenMoneyAdded = true
            };
            accountInStore2 = new Account
            {
                Id = 98,
                Number = 235,
                Name = "Account2",
                IncreasesDebitWhenMoneyAdded = false
            };
            transactionInStore1 = new Transaction
            {
                Id = 458,
                JournalId = journalInStore.Id,
                AccountId = accountInStore1.Id,
                Amount = (decimal)205.25
            };
            transactionInStore2 = new Transaction
            {
                Id = 987,
                JournalId = journalInStore.Id,
                AccountId = accountInStore2.Id,
                Amount = (decimal)587.15
            };
            context.AddRange(new[] { accountInStore1, accountInStore2 });
            context.AddRange(new[] { transactionInStore1, transactionInStore2 });
        }

        [TestMethod]
        public async Task DeleteJournalsAsyncDeletesJournal()
        {
            var context = InMemoryContext.GetContext();

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

            await context.AddRangeAsync(new[] { journalInStoreToDelete, journalInStoreToKeep });

            await context.SaveChangesAsync();
            var transactionService = new TransactionService(context);

            await transactionService.DeleteJournalAsync(journalInStoreToDelete.Id);

            var journalsAfterDelete = await context.Journals.ToListAsync();

            Assert.AreEqual(0, journalsAfterDelete.Count(j => j.Id == journalInStoreToDelete.Id));
            Assert.AreEqual(1, journalsAfterDelete.Count(j => j.Id == journalInStoreToKeep.Id));
        }

        [TestMethod]
        public async Task AddJournalAsyncAddsJournal()
        {
            var context = InMemoryContext.GetContext();
            MapsterConfig.ConfigureMapster();

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
            await context.AddRangeAsync(new[]
            {
                accountInStore1,accountInStore2
            });
            await context.SaveChangesAsync();

            var transactionService = new TransactionService(context);

            var journalToAdd = new JournalAddDto
            {
                Description = "SomeDescription1",
                Date = new DateTime(2020, 10, 15),
                Transactions = new TransactionAddDto[]
                {
                    new()
                    {
                        Amount = (decimal)205.15,
                        AccountId = accountInStore1.Id,
                    },
                    new()
                    {
                        Amount = (decimal)698.50,
                        AccountId = accountInStore2.Id,
                    }
                }
            };

            var journalAdded = await transactionService.AddJournalAsync(journalToAdd);

            var journals = await context.Journals.ToListAsync();

            var journalsAfterAdd = await context.Journals
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
}