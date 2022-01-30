using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;

namespace ShoppingMall.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingmallContext _context;

        public readonly DbSet<Product> _products;

        public ProductService(ShoppingmallContext context)
        {
            _context = context;
            _products = _context.Product;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _products.Include(d => d.Catalog).ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _products.Include(d => d.Catalog).FirstOrDefaultAsync(d => d.Id == id);
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
            var orignalProduct = _products.Find(product.Id);

            if (orignalProduct == null)
            {
                return null;
            }

            orignalProduct.CatalogId = product.CatalogId;
            orignalProduct.Count = product.Count;
            orignalProduct.Description = product.Description;
            orignalProduct.Name = product.Name;
            orignalProduct.PicturePath = product.PicturePath;
            orignalProduct.Summary = product.Summary;
            orignalProduct.UnitPrice = product.UnitPrice;

            await _context.SaveChangesAsync();

            return orignalProduct;
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
    }
}
