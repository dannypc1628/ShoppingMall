using ShoppingMall.Models;
using ShoppingMall.Repositories;

namespace ShoppingMall.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _products;

        public ProductService(IProductRepository productRepository)
        {
            _products = productRepository;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _products.GetAllAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _products.GetAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _products.CreateAsync(product);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _products.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _products.DeleteAsync(id);
        }

        public async Task<IList<Product>> FilterAsync(int pageNumber, int pageSize)
        {
            return await _products.FilterAsync(pageNumber, pageSize);
        }
    }
}
