using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Repository;
using Yadebs.Db;
using Yadebs.Db.Specifications;
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
        await _repository.UpdateS(journal, new JournalWithTransactionsSpec());
        return await GetJournalAsync(id);
    }

    public async Task<JournalDto> GetJournalAsync(int id)
        => await _repository.GetAsyncS(id, new JournalWithTransactionsAndAccountsSpec());
}