using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;

namespace ShoppingMall.Services
{
    public class ProductServices
    {
        private readonly ShoppingmallContext _context;

        public readonly DbSet<Product> _products;

        public ProductServices(ShoppingmallContext context)
        {
            _context = context;
            _products = _context.Product;
        }

        public IList<Product> GetAll()
        {
            return _products.ToList();
        }

        public Product Get(Guid id)
        {
            return _products.Find(id);
        }

        public Product Create(Product product)
        {
            product.Id = Guid.NewGuid();

            _products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public Product Update(Product product)
        {
            var orignalProduct = _products.Find(product.Id);

            if (orignalProduct == null) return null;

            orignalProduct.CatalogId = product.CatalogId;
            orignalProduct.Count = product.Count;
            orignalProduct.Description = product.Description;
            orignalProduct.Name = product.Name;
            orignalProduct.Summary = product.Summary;
            orignalProduct.UnitPrice = product.UnitPrice;

            _context.SaveChanges();

            return orignalProduct;
        }

        public bool Delete(Guid id)
        {
            var product = _products.Find(id);
            if (product == null) return false;

            try
            {
                _products.Remove(product);
                _context.SaveChanges(true);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}
