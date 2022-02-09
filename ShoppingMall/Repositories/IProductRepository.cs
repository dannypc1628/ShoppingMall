using ShoppingMall.Models;

namespace ShoppingMall.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IList<Product>> FilterAsync(int pageNumber, int pageSize);
    }
}
