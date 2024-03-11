using Mapster;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll;

public static class MapsterConfig
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig<Journal, JournalDto>.NewConfig().MaxDepth(3);
    }
}