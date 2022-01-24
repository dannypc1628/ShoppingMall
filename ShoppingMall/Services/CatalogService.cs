using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;

namespace ShoppingMall.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ShoppingmallContext _context;

        public readonly DbSet<Catalog> _catalogs;

        public CatalogService(ShoppingmallContext context)
        {
            _context = context;
            _catalogs = _context.Catalog;
        }
        public async Task<Catalog> CreateAsync(Catalog entity)
        {
            _catalogs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var catalog = await _catalogs.FindAsync(id);
            if (catalog == null)
            {
                return false;
            }

            try
            {
                _catalogs.Remove(catalog);
                await _context.SaveChangesAsync(true);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public async Task<IList<Catalog>> GetAllAsync()
        {
            return await _catalogs.Include(d => d.Parent).ToListAsync();
        }

        public async Task<Catalog> GetAsync(int id)
        {
            return await _catalogs.Include(d => d.Parent).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Catalog> UpdateAsync(Catalog entity)
        {
            var catalog = await GetAsync(entity.Id);

            if (catalog == null)
            {
                return null;
            }

            catalog.Name = entity.Name;
            catalog.ParentId = entity.ParentId;
            await _context.SaveChangesAsync();

            return catalog;
        }
    }
}
