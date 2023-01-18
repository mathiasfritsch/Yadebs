using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yadebs.Db;
using Yadebs.Models.Dto.Journal;

namespace Yadebs.Bll
{
    public static class MapsterConfig
    {
        public static void ConfigureMapster()
        {
            TypeAdapterConfig<Journal, JournalDto>.NewConfig().MaxDepth(3);
            TypeAdapterConfig<JournalUpdateDto, Journal>.NewConfig();
            TypeAdapterConfig<TransactionUpdateDto, Transaction>.NewConfig();
        }
    }
}