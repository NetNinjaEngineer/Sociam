using Microsoft.AspNetCore.Http;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Interfaces.Services;
public interface IFileService
{
    Task<(bool uploaded, string? fileName)> UploadFileAsync(IFormFile? file, string folderName);
    Task<IEnumerable<FileUploadResult>> UploadFilesParallelAsync(
        IEnumerable<IFormFile> files,
        string? folderName = null, CancellationToken cancellationToken = default);

    bool DeleteFileFromPath(string filePath, string locationFolder);
}
