using Mapster;
using Microsoft.EntityFrameworkCore;
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
            var account = accountDto.Adapt<Account>();
            await this.context.Accounts.AddAsync(account);
            await this.context.SaveChangesAsync();
            return await this.GetAccountAsync(account.Id);
        }

        public async Task UpdateAccountAsync(int id, AccountDto accountDto)
        {
            var accountToUpdate = await this.context.Accounts.SingleAsync(a => a.Id == id);
            accountDto.Adapt(accountToUpdate);

            await this.context.SaveChangesAsync();
        }

        public async Task<AccountDto> GetAccountAsync(int id) =>
            (await this.context.Accounts.SingleAsync(a => a.Id == id))
                .Adapt<AccountDto>();

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync() =>
            await this.context
                .Accounts
                .OrderBy(a => a.Number)
                .ProjectToType<AccountDto>()
                .ToListAsync();

        public async Task DeleteAccountAsync(int id)
        {
            var account = await this.context.Accounts.SingleAsync(a => a.Id == id);

            var childAccounts = await this.context.Accounts.Where(a => a.ParentId == id).ToListAsync();
            childAccounts.ForEach(a => a.ParentId = account.ParentId);

            this.context.Accounts.Remove(account);
            await this.context.SaveChangesAsync();
        }
    }
}