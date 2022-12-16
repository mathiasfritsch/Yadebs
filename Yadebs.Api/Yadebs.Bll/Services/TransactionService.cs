using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Interfaces;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services
{
    public class TransactionService : ITransactionService
    {
        private AccountingContext context;

        public TransactionService(AccountingContext context)
        {
            this.context = context;
        }

        public async Task<JournalDto> AddJournalAsync(JournalDto journal)
        {
            await this.context.SaveChangesAsync();
            return await GetJournalAsync(1);
        }

        public async Task<JournalDto> GetJournalAsync(int id)
        {
            var journal = await this.context.Journals
                .Include(j => j.Transactions)
                .ThenInclude(t => t.Account)
                .SingleAsync(j => j.Id == id);

            return journal.Adapt<JournalDto>();
        }
    }
}