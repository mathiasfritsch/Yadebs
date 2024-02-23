using Ardalis.Specification;

namespace Yadebs.Bll.Repository;

public interface IRepository<T, TDto, TUpdate, TAdd>
    where T : class, IEntity
    where TDto : class, IEntity
    where TUpdate : class, IEntity
    where TAdd : class
{
    public Task<TDto> GetAsync(int id);
    public Task<TDto> GetAsyncS(int id, ISpecification<T> specification);
    public Task<List<TDto>> GetAllAsync();
    public Task<List<TDto>> GetAllAsyncS(ISpecification<T> specification);
    public Task Delete(int id);
    public Task Update(TUpdate updateEntity);
    public Task UpdateS(TUpdate updateEntity, ISpecification<T> specification);
    public Task<TDto> Add(TAdd addEntity);
}