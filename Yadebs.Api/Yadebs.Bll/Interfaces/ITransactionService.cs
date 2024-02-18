using Yadebs.Models.Dto;

namespace Yadebs.Bll.Interfaces;

public interface ITransactionService
{
    public Task<JournalDto> GetJournalAsync(int id);

    public Task DeleteJournalAsync(int id);

    public Task<List<JournalDto>> GetJournalsAsync();

    public Task<JournalDto> AddJournalAsync(JournalAddDto journal);

    public Task<JournalDto> UpdateJournalAsync(int id, JournalUpdateDto journal);
}