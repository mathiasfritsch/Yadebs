using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Interfaces;
using Yadebs.Db;
using Yadebs.Models.Dto;
using Yadebs.Models.Dto.Journal;

namespace Yadebs.Bll.Services
{
    public class TransactionService : ITransactionService
    {
        private AccountingContext context;

        public TransactionService(AccountingContext context)
        {
            this.context = context;
        }

        public async Task DeleteJournalAsync(int id)
        {
            var journal = await this.context.Journals.SingleAsync(a => a.Id == id);
            this.context.Journals.Remove(journal);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<JournalDto>> GetJournalsAsync() =>
            await this.context
                .Journals
                .OrderBy(a => a.Date)
                .ProjectToType<JournalDto>()
                .ToListAsync();

        public async Task<JournalDto> AddJournalAsync(JournalDto journalDto)
        {
            var journal = journalDto.Adapt<Journal>();
            await this.context.Journals.AddAsync(journal);
            await this.context.SaveChangesAsync();
            return  await this.GetJournalAsync(journal.Id);
        }

        public async Task<JournalDto> UpdateJournalAsync(int id, JournalUpdateDto journal)
        {
            var journalToUpdate = await this
                .context
                .Journals
                .Include(t => t.Transactions)
                .SingleAsync(a => a.Id == id);

            journal.Adapt(journalToUpdate);

            journalToUpdate.Transactions[0].Amount = journal.Transactions[0].Amount;
            journalToUpdate.Transactions[1].Amount = journal.Transactions[1].Amount;
            journalToUpdate.Transactions[0].AccountId = journal.Transactions[0].AccountId;
            journalToUpdate.Transactions[1].AccountId = journal.Transactions[1].AccountId;

            await this.context.SaveChangesAsync();

            return await GetJournalAsync(id);
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