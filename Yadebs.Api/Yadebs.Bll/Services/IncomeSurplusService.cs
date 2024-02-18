using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Bll.Interfaces;
using Yadebs.Db;
using Yadebs.Db.IncomeSurplusCalculation;
using Yadebs.Models.Dto;

namespace Yadebs.Bll.Services;

public class IncomeSurplusService : IIncomeSurplusService
{
    private AccountingContext context;

    public IncomeSurplusService(AccountingContext context)
    {
        this.context = context;
    }

    public async Task<List<BankTransferDto>> GetBankTransfersAsync() =>
        await this.context
            .Journals
            .OrderBy(a => a.Date)
            .ProjectToType<BankTransferDto>()
            .ToListAsync();

    public async Task<BankTransferDto> AddBankTransfer(BankTransferAddDto bankTransferAdd)
    {
        var bankTransfer = bankTransferAdd.Adapt<BankTransfer>();
        await this.context.BankTransfers.AddAsync(bankTransfer);
        await this.context.SaveChangesAsync();
        return await this.GetBankTransferAsync(bankTransfer.Id);
    }

    public async Task<BankTransferDto> GetBankTransferAsync(int id)
    {
        var bankTransfer = await this.context.BankTransfers
            .SingleAsync(j => j.Id == id);

        return bankTransfer.Adapt<BankTransferDto>();
    }

    public async Task DeleteBankTransferAsync(int id)
    {
        var journal = await this.context.BankTransfers.SingleAsync(a => a.Id == id);
        this.context.BankTransfers.Remove(journal);
        await this.context.SaveChangesAsync();
    }

    public async Task<BankTransferDto> UpdateBankTransferAsync(int id, BankTransferUpdateDto bankTransfer)
    {
        var bankTransferToUpdate = await this
            .context
            .BankTransfers
            .SingleAsync(a => a.Id == id);

        bankTransfer.Adapt(bankTransferToUpdate);

        await this.context.SaveChangesAsync();

        return await GetBankTransferAsync(id);
    }
}