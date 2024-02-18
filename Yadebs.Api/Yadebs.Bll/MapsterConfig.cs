using Mapster;
using Yadebs.Db;
using Yadebs.Db.IncomeSurplusCalculation;
using Yadebs.Models.Dto;

namespace Yadebs.Bll
{
    public static class MapsterConfig
    {
        public static void ConfigureMapster()
        {
            TypeAdapterConfig<Journal, JournalDto>.NewConfig().MaxDepth(3);
            TypeAdapterConfig<JournalUpdateDto, Journal>.NewConfig();
            TypeAdapterConfig<JournalAddDto, Journal>.NewConfig();

            TypeAdapterConfig<TransactionUpdateDto, Transaction>.NewConfig();
            TypeAdapterConfig<TransactionAddDto, Transaction>.NewConfig();
            TypeAdapterConfig<Transaction, TransactionDto>.NewConfig();
            TypeAdapterConfig<BankTransfer, BankTransferDto>.NewConfig();
        }
    }
}