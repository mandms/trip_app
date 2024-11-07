using Application.Dto.Image;

namespace Application.Services.FileService
{
    public class FileService : IFileService
    {
        private static string _contentRootPath = Path.Combine(Directory.GetCurrentDirectory());
        public FileService()
        {
        }

        private string GetFileExtension(string base64Image)
        {
            var extensionData = base64Image.Substring(0, 5);
            switch (extensionData.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAABA":
                    return "ico";
                default:
                    return string.Empty;
            }
        }

        public async Task<string> SaveFileAsync(string base64Image, string[] allowedFileExtensions)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                throw new ArgumentNullException("Image string is empty");
            }

            var path = Path.Combine(_contentRootPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = GetFileExtension(base64Image);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }

            var fileName = $"{Guid.NewGuid().ToString()}.{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            await File.WriteAllBytesAsync(fileNameWithPath, imageBytes);
            return fileName;
        }


        public void DeleteFile(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            var path = Path.Combine(_contentRootPath, $"Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }
            File.Delete(path);
        }

        public List<Task<CreateImageDto>> SaveImages(List<CreateImageDto> createImageDtos)
        {
            return createImageDtos.Select(async image =>
             {
                 image.Path = await SaveFileAsync(image.Image, ["jpg", "png", "jpeg"]);
                 return image;
             }).ToList();
        }
    }
}
