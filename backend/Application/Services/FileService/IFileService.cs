using Application.Dto.Image;

namespace Application.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string base64Image, string[] allowedFileExtensions, CancellationToken cancellationToken);
        void DeleteFile(string fileNameWithExtension);
        List<Task<CreateImageDto>> SaveImages(List<CreateImageDto> createImageDtos, CancellationToken cancellationToken);
    }
}
