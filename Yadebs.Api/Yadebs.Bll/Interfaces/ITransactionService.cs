using Yadebs.Models.Dto.Journal;

namespace Yadebs.Bll.Interfaces
{
    public interface ITransactionService
    {
        public Task<JournalDto> GetJournalAsync(int id);

        public Task DeleteJournalAsync(int id);

        public Task<List<JournalDto>> GetJournalsAsync();

        public Task<JournalDto> AddJournalAsync(JournalDto journal);

        public Task<JournalDto> UpdateJournalAsync(int id,JournalUpdateDto journal);
    }
}