using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Services;
using Yadebs.Db;

namespace Yadebs.Bll.Tests
{
    [TestClass]
    public class AccountingServiceTests
    {
        [TestMethod]
        public async Task GetAccountsGetsResultsAsync()
        {
            var context = InMemoryContext.GetContext();

            context.Add(
                new Account
                {
                    Id = 13,
                    BookId = 1,
                    Name = "SomeAccount1",
                    Number = 123445
                }
                );
            context.Add(
                new Account
                {
                    Id = 21,
                    BookId = 1,
                    Name = "SomeAccount2",
                    Number = 6587,
                    ParentId = 13
                }
                );
            await context.SaveChangesAsync();
            AccountingService accountingService = new AccountingService(context);

            var accounts = await accountingService.GetAccountsAsync();

            Assert.AreEqual(2, accounts.Count());
        }

        [TestMethod]
        public async Task GetAccountGetsResult()
        {
            var context = InMemoryContext.GetContext();

            context.Add(
                new Account
                {
                    Id = 13,
                    BookId = 1,
                    Name = "SomeAccount1",
                    Number = 123445
                }
                );

            await context.SaveChangesAsync();
            AccountingService accountingService = new AccountingService(context);

            var account = await accountingService.GetAccountAsync(13);

            Assert.AreEqual(13, account.Id);
            Assert.AreEqual(1, account.BookId);
            Assert.AreEqual("SomeAccount1", account.Name);
            Assert.AreEqual(123445, account.Number);
        }

        [TestMethod]
        public async Task AddAccountAddsAccount()
        {
            var context = InMemoryContext.GetContext();
            var accountingService = new AccountingService(context);

            var accountAdd = new Models.Dto.AccountDto
            {
                Id = 13,
                BookId = 1,
                Name = "SomeAccount1",
                Number = 123445,
                ParentId = 256
            };

            _ = await accountingService.AddAccountAsync(accountAdd);

            var accountReload = await accountingService.GetAccountAsync(accountAdd.Id);

            Assert.AreEqual(accountAdd.Id, accountReload.Id);
            Assert.AreEqual(accountAdd.BookId, accountReload.BookId);
            Assert.AreEqual(accountAdd.Name, accountReload.Name);
            Assert.AreEqual(accountAdd.Number, accountReload.Number);
            Assert.AreEqual(accountAdd.ParentId, accountReload.ParentId);

        }

        [TestMethod]
        public async Task UpdateAccountUpdatesAccount()
        {
            var context = InMemoryContext.GetContext();

            var account = new Account
            {
                Id = 13,
                BookId = 1,
                Name = "SomeAccount1",
                Number = 123445
            };

            context.Add(account);

            await context.SaveChangesAsync();
            AccountingService accountingService = new AccountingService(context);

            var accountToUpdate = await accountingService.GetAccountAsync(account.Id);

            accountToUpdate.ParentId = 58;
            accountToUpdate.BookId = 2;
            accountToUpdate.Number = 54987;
            accountToUpdate.Name = "SomeUpdatedAccount";

            await accountingService.UpdateAccountAsync(account.Id, accountToUpdate);

            var accountReload = await accountingService.GetAccountAsync(account.Id);

            Assert.AreEqual(accountToUpdate.Id, accountReload.Id);
            Assert.AreEqual(accountToUpdate.BookId, accountReload.BookId);
            Assert.AreEqual(accountToUpdate.Name, accountReload.Name);
            Assert.AreEqual(accountToUpdate.Number, accountReload.Number);
        }

        [TestMethod]
        public async Task DeleteAccountDeletesAccount()
        {
            var context = InMemoryContext.GetContext();

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

            context.AddRange(new[] { accountToStay, accountToDelete });

            await context.SaveChangesAsync();
            var accountingService = new AccountingService(context);

            await accountingService.DeleteAccountAsync(accountToDelete.Id);

            var accountsInStore = await context.Accounts.ToListAsync();

            Assert.AreEqual(0,  accountsInStore.Count( a => a.Id == accountToDelete.Id));
            Assert.AreEqual(1, accountsInStore.Count(a => a.Id == accountToStay.Id));
        }
    }
}