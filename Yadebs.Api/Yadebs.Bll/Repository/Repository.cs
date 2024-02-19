using Mapster;
using Microsoft.EntityFrameworkCore;
using Yadebs.Db;

namespace Yadebs.Bll.Repository
{
    public class Repository<T, TDto, TUpdate, TAdd>(AccountingContext context) : IRepository<T, TDto, TUpdate, TAdd>
        where T : class, IEntity
        where TDto : class, IEntity
        where TUpdate : class, IEntity
        where TAdd : class
    {
        public async Task<TDto> GetAsync(int id) =>
            (await context.Set<T>().SingleAsync(e => e.Id == id))
            .Adapt<TDto>();

        public async Task<List<TDto>> GetAllAsync() =>
            await context.Set<T>().ProjectToType<TDto>().ToListAsync();

        public async Task Delete(int id)
        {
            context.Set<T>().Remove(await context.Set<T>().SingleAsync(e => e.Id == id));
            await context.SaveChangesAsync();
        }
        public async Task<TDto> Add(TAdd addDto)
        {
            var addEntity = addDto.Adapt<T>();
            await context.AddAsync(addEntity);
            await context.SaveChangesAsync();
            return (await context.Set<T>().SingleAsync(e => e.Id == addEntity.Id))
                .Adapt<TDto>();
        }

        public async Task Update(TUpdate updateEntity)
        {
            var entity = await context.Set<T>().SingleAsync(e => e.Id == updateEntity.Id);
            updateEntity.Adapt(entity);
            await context.SaveChangesAsync();
        }
    }
}
