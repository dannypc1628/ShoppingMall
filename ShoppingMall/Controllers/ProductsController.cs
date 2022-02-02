using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;

namespace ShoppingMall.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingmallContext _context;

        public ProductsController(ShoppingmallContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var shoppingmallContext = _context.Product.Include(p => p.Catalog);
            return View(await shoppingmallContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Catalog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
