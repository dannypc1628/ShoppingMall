using ShoppingMall.Models;
using X.PagedList;

namespace ShoppingMall.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IPagedList<Product>> FilterAsync(int pageNumber, int pageSize);
    }
}
