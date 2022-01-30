#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingMall.Extensions;
using ShoppingMall.Models;
using ShoppingMall.Models.ViewModels;
using ShoppingMall.Services;

namespace ShoppingMall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ShoppingmallContext _context;
        private readonly IProductService _productServices;

        public ProductsController(ShoppingmallContext context, IProductService productServices, IFileService fileService)
        {
            _fileService = fileService;
            _context = context;
            _productServices = productServices;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _productServices.GetAllAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productServices.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var picturePath = string.Empty;
                if (productViewModel.Picture != null)
                {
                    picturePath = await _fileService.CreateAsync(productViewModel.Picture);
                }

                await _productServices.CreateAsync(productViewModel.ToProduct(picturePath));
                return RedirectToAction(nameof(Index));
            }

            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", productViewModel.CatalogId);
            return View(productViewModel);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productServices.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", product.CatalogId);
            return View(product.ToProductViewModel());
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Summary,Description,UnitPrice,Count,CatalogId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", product.CatalogId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
