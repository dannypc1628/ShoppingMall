using ShoppingMall.Models;

namespace ShoppingMall.Services
{
    public interface IProductService : IService<Product, Guid>
    {
        Task<IList<Product>> FilterAsync(int pageNumber, int pageSize);
    }
}
