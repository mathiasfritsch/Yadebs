using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Repository;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services;

public class TransactionService : ITransactionService
{
    private AccountingContext context;
    private IRepository<Journal, JournalDto, JournalUpdateDto, JournalAddDto> _repository;

    public TransactionService(AccountingContext context)
    {
        this.context = context;
    }

    public TransactionService(AccountingContext context,
        IRepository<Journal, JournalDto, JournalUpdateDto, JournalAddDto> repository)
    {
        this.context = context;
        _repository = repository;
    }

    public async Task DeleteJournalAsync(int id)
        => await _repository.Delete(id);

    public async Task<List<JournalDto>> GetJournalsAsync()
        => await _repository.GetAllAsync();

    public async Task<JournalDto> AddJournalAsync(JournalAddDto journalAdd)
        => await _repository.Add(journalAdd);


    public async Task<JournalDto> UpdateJournalAsync(int id, JournalUpdateDto journal)
    {
        var journalToUpdate = await this
            .context
            .Journals
            .Include(t => t.Transactions)
            .SingleAsync(a => a.Id == id);

        journal.Adapt(journalToUpdate);

        //journalToUpdate.Transactions[0].Amount = journal.Transactions[0].Amount;
        //journalToUpdate.Transactions[1].Amount = journal.Transactions[1].Amount;
        //journalToUpdate.Transactions[0].AccountId = journal.Transactions[0].AccountId;
        //journalToUpdate.Transactions[1].AccountId = journal.Transactions[1].AccountId;

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