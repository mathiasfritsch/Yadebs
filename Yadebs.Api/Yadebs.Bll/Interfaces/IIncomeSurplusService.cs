using Yadebs.Models.Dto;

namespace Yadebs.Bll.Interfaces;

public interface IIncomeSurplusService
{
    Task<BankTransferDto> GetBankTransferAsync(int bankTransferId);
    public Task<List<BankTransferDto>> GetBankTransfersAsync();
    public Task<BankTransferDto> AddBankTransfer(BankTransferAddDto bankTransferAdd);
    public Task DeleteBankTransferAsync(int id);
    public Task<BankTransferDto> UpdateBankTransferAsync(int id, BankTransferUpdateDto bankTransfer);
}