using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yadebs.Db;
using Yadebs.Models.Dto;

namespace Yadebs.Bll
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Journal, JournalDto>.NewConfig().Fork(config => config.Default.PreserveReference(true));
        }
    }
}
