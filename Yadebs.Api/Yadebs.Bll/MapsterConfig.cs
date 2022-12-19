using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Journal, JournalDto>.NewConfig().MaxDepth(3);
        }
    }
}