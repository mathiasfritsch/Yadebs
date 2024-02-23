using Yadebs.Bll.Interfaces;
using Yadebs.Bll.Repository;
using Yadebs.Db;
using Yadebs.Db.IncomeSurplusCalculation;
using Yadebs.Db.IncomeSurplusCalculation.Specifications;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services;

public class IncomeSurplusService : IIncomeSurplusService
{
    private readonly AccountingContext _context;
    private IRepository<BankTransfer, BankTransferDto, BankTransferUpdateDto, BankTransferAddDto> _repository;
    public IncomeSurplusService(AccountingContext context,
        IRepository<BankTransfer, BankTransferDto, BankTransferUpdateDto, BankTransferAddDto> repository)
    {
        this._context = context;
        this._repository = repository;
    }

    public async Task<List<BankTransferDto>> GetBankTransfersAsync() =>
        await this._repository.GetAllAsyncS(new BankTransfersOrderedByDateSpec());

    public async Task<BankTransferDto> AddBankTransfer(BankTransferAddDto bankTransferAdd)
    {
        var bankTransfer = await _repository.Add(bankTransferAdd);
        return await this.GetBankTransferAsync(bankTransfer.Id);
    }

    public async Task<BankTransferDto> GetBankTransferAsync(int id)
        => await _repository.GetAsync(id);

    public async Task DeleteBankTransferAsync(int id)
        => await _repository.Delete(id);

    public async Task<BankTransferDto> UpdateBankTransferAsync(int id, BankTransferUpdateDto bankTransfer)
    {
        await _repository.Update(bankTransfer);
        return await GetBankTransferAsync(id);
    }
}