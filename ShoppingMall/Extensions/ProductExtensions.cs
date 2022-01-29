using ShoppingMall.Models;
using ShoppingMall.Models.ViewModels;

namespace ShoppingMall.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductViewModel productViewModel)
        {
            return new Product()
            {
                CatalogId = productViewModel.CatalogId,
                Count = productViewModel.Count,
                Description = productViewModel.Description,
                Name = productViewModel.Name,
                Summary = productViewModel.Summary,
                UnitPrice = productViewModel.UnitPrice,
            };
        }
    }
}
