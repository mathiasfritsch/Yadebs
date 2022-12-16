using Yadebs.Models.Dto;

namespace Yadebs.Bll.Interfaces
{
    public interface ITransactionService
    {
        public  Task<JournalDto> GetJournalAsync(int id);
    }
}