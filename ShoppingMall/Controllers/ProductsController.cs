using Microsoft.AspNetCore.Mvc;
using ShoppingMall.Services;

namespace ShoppingMall.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            return View(await _productService.FilterAsync(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
