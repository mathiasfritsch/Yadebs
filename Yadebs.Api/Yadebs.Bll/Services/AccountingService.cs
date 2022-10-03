using Microsoft.EntityFrameworkCore;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services
{
    public class AccountingService : IAccountingService
    {
        AccountingContext context;

        public AccountingService(AccountingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
        {
    
            return await this.context.Accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                Number = a.Number,
            }).ToListAsync();
        }
    }
}