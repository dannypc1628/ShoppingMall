using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShoppingMall.Models;

namespace ShoppingMall.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingmallContext _context;

        private readonly DbSet<Product> _products;

        public ProductRepository(ShoppingmallContext context)
        {
            _context = context;
            _products = _context.Product;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await GetAllQuery().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            _products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var originalProduct = await _products.FindAsync(product.Id);

            if (originalProduct == null)
            {
                return null;
            }

            originalProduct.CatalogId = product.CatalogId;
            originalProduct.Count = product.Count;
            originalProduct.Description = product.Description;
            originalProduct.Name = product.Name;
            originalProduct.PicturePath = product.PicturePath;
            originalProduct.Summary = product.Summary;
            originalProduct.UnitPrice = product.UnitPrice;

            await _context.SaveChangesAsync();

            return originalProduct;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            try
            {
                _products.Remove(product);
                await _context.SaveChangesAsync(true);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        private IIncludableQueryable<Product, Catalog> GetAllQuery()
        {
            return _products.Include(d => d.Catalog);
        }
    }
}
