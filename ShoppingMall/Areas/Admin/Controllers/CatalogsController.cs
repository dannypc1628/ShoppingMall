#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;

namespace ShoppingMall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogsController : Controller
    {
        private readonly ShoppingmallContext _context;

        public CatalogsController(ShoppingmallContext context)
        {
            _context = context;
        }

        // GET: Admin/Catalogs
        public async Task<IActionResult> Index()
        {
            var shoppingmallContext = _context.Catalog.Include(c => c.Parent);
            return View(await shoppingmallContext.ToListAsync());
        }

        // GET: Admin/Catalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Admin/Catalogs/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Catalog, "Id", "Name");
            return View();
        }

        // POST: Admin/Catalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Catalog, "Id", "Name", catalog.ParentId);
            return View(catalog);
        }

        // GET: Admin/Catalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Catalog, "Id", "Name", catalog.ParentId);
            return View(catalog);
        }

        // POST: Admin/Catalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ParentId")] Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Catalog, "Id", "Name", catalog.ParentId);
            return View(catalog);
        }

        // GET: Admin/Catalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Admin/Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalog = await _context.Catalog.FindAsync(id);
            _context.Catalog.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalog.Any(e => e.Id == id);
        }
    }
}
