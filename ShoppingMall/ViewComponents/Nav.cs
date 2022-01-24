using Microsoft.AspNetCore.Mvc;
using ShoppingMall.Services;

namespace ShoppingMall.ViewComponents
{
    public class Nav : ViewComponent

    {
        private readonly ICatalogService _catalogService;

        public Nav(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _catalogService.GetAllAsync();
            return View(items);
        }
    }
}
