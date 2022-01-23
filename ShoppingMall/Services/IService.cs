namespace ShoppingMall.Services
{
    public interface IService<T, TId>
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetAsync(TId id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(TId id);
    }
}
