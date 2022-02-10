using ShoppingMall.Models;
using X.PagedList;

namespace ShoppingMall.Services
{
    public interface IProductService : IService<Product, Guid>
    {
        Task<IPagedList<Product>> FilterAsync(int pageNumber, int pageSize);
    }
}
