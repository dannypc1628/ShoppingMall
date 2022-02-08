namespace ShoppingMall.Repositories
{
    public interface IRepository<T, TId>
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetAsync(TId id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(TId id);
    }
}
