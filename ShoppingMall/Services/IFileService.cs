namespace ShoppingMall.Services
{
    public interface IFileService
    {
        Task<string> CreateAsync(IFormFile formFile);
    }
}
