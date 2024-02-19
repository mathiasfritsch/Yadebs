namespace Yadebs.Bll.Repository;

public interface IRepository<T, TDto, TUpdate, TAdd>
    where T : class, IEntity
    where TDto : class, IEntity
    where TUpdate : class, IEntity
    where TAdd : class
{
    public Task<TDto> GetAsync(int id);
    public Task<List<TDto>> GetAllAsync();
    public Task Delete(int id);
    public Task Update(TUpdate updateEntity);
    public Task<TDto> Add(TAdd addEntity);
}