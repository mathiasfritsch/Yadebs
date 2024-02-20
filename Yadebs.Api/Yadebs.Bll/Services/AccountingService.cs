using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Repository;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services;

public class AccountingService : IAccountingService
{
    private AccountingContext context;
    private IRepository<Account, AccountDto, AccountUpdateDto, AccountAddDto> _repository;


    public AccountingService(AccountingContext context)
    {
        this.context = context;

    }

    public AccountingService(AccountingContext context,
        IRepository<Account, AccountDto, AccountUpdateDto, AccountAddDto> repository)
    {
        this.context = context;
        _repository = repository;
    }

    public async Task<AccountDto> AddAccountAsync(AccountAddDto accountDto)
        => await _repository.Add(accountDto);

    public async Task UpdateAccountAsync(int id, AccountUpdateDto accountUpdate)
        => await _repository.Update(accountUpdate);

    public async Task<AccountDto> GetAccountAsync(int id)
        => await _repository.GetAsync(id);

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
        => await _repository.GetAllAsync();

    public async Task DeleteAccountAsync(int id)
    {
        var account = await _repository.GetAsync(id);

        var childAccounts = await this.context.Accounts.Where(a => a.ParentId == id).ToListAsync();
        childAccounts.ForEach(a => a.ParentId = account.ParentId);
        await this.context.SaveChangesAsync();

        await _repository.Delete(id);
    }
}