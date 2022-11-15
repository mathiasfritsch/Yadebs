using Yadebs.Models.Dto;

namespace Yadebs.Bll
{
    public interface IAccountingService
    {
        public Task<IEnumerable<AccountDto>> GetAccountsAsync();
        public Task<AccountDto> AddAccountAsync(AccountDto accountDto);
        public Task<AccountDto> GetAccountAsync(int id);
        public Task DeleteAccountAsync(int id);
        public Task UpdateAccountAsync(int id, AccountDto accountDto);
    }
}