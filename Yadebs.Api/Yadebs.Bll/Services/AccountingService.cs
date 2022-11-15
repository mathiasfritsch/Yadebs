using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services
{
    public class AccountingService : IAccountingService
    {
        private AccountingContext context;

        public AccountingService(AccountingContext context)
        {
            this.context = context;
        }

        public async Task<AccountDto> AddAccountAsync(AccountDto accountDto)
        {
            var account = new Account
            {
                BookId = accountDto.BookId,
                Name = accountDto.Name,
                Number = accountDto.Number
            };
            this.context.Accounts.Add(account);
            await this.context.SaveChangesAsync();
            return await GetAccountAsync(account.Id);
        }

        public async Task UpdateAccountAsync(int id, AccountDto accountDto)
        {
            var accountToUpdate =  await this.context.Accounts.SingleAsync(a => a.Id == id);

            accountToUpdate.BookId = accountDto.BookId;
            accountToUpdate.Name = accountDto.Name;
            accountToUpdate.Number = accountDto.Number;
            accountToUpdate.ParentId = accountDto.ParentId;

            await this.context.SaveChangesAsync();
        }

        public async Task<AccountDto> GetAccountAsync(int id)
        {
            var account = await this.context.Accounts.SingleAsync(a => a.Id == id);
            return new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Number = account.Number,
                BookId = account.BookId
            };
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()

        {
            return await this.context
                .Accounts
                .OrderBy(a => a.Number)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Number = a.Number,
                    BookId = a.BookId,
                    ParentId = a.ParentId
                }).ToListAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await this.context.Accounts.SingleAsync(a => a.Id == id);

            var childAccounts = await this.context.Accounts.Where(a => a.ParentId == id ).ToListAsync();
            childAccounts.ForEach(a => a.ParentId = account.ParentId);

            this.context.Accounts.Remove(account);
            await this.context.SaveChangesAsync();
        }
    }
}