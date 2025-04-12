using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Interfaces.Services;
public interface IFileService
{
    Task<(bool uploaded, string? fileName)> UploadFileAsync(IFormFile? file, string folderName);
    Task<IEnumerable<FileUploadResult>> UploadFilesParallelAsync(IEnumerable<IFormFile> files, string? folderName = null, CancellationToken cancellationToken = default);
    bool DeleteFileFromPath(string filePath, string locationFolder);
    Task<Result<CloudinaryUploadResult>> CloudinaryUploadSingleFileAsync(IFormFile file);
    Task<Result<List<CloudinaryUploadResult>>> CloudinaryUploadMultipleFilesAsync(IFormFileCollection files);
    Task<Result<object>> GetResourceAsync(string assetId);
    Task<Result<bool>> DeleteCloudinaryResourceAsync(string publicId, FileType fileType);
}
