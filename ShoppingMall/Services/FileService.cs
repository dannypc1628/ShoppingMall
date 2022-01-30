namespace ShoppingMall.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvionment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvionment = webHostEnvironment;
        }

        public async Task<string> CreateAsync(IFormFile formFile)
        {
            var fileName = Guid.NewGuid().ToString();
            var fileExtension = Path.GetExtension(formFile.FileName);
            var picturePath = $"/img/{fileName}{fileExtension}";
            using (var stream = System.IO.File.Create($"{_webHostEnvionment.WebRootPath}{picturePath}"))
            {
                await formFile.CopyToAsync(stream);
            }
            return picturePath;
        }
    }
}
