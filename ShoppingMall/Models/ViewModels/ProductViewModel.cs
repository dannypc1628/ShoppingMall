namespace ShoppingMall.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
        public int CatalogId { get; set; }

        public string PicturePath { get; set; }

        public IFormFile Picture { get; set; }

    }
}
