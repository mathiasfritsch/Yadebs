using Yadebs.Models.Dto;

namespace Yadebs.Bll
{
    public interface IAccountingService
    {
        public Task<IEnumerable<AccountDto>> GetAccountsAsync();
    }
}