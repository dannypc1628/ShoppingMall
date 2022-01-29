using ShoppingMall.Models;
using ShoppingMall.Models.ViewModels;

namespace ShoppingMall.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductViewModel productViewModel, string picturePath)
        {
            return new Product()
            {
                CatalogId = productViewModel.CatalogId,
                Count = productViewModel.Count,
                Description = productViewModel.Description,
                Name = productViewModel.Name,
                PicturePath = string.IsNullOrWhiteSpace(picturePath) ? string.Empty : picturePath,
                Summary = productViewModel.Summary,
                UnitPrice = productViewModel.UnitPrice,
            };
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                CatalogId = product.CatalogId,
                Count = product.Count,
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                PicturePath = string.IsNullOrWhiteSpace(product.PicturePath) ? string.Empty : product.PicturePath,
                Summary = product.Summary,
                UnitPrice = product.UnitPrice,
            };
        }

    }
}
