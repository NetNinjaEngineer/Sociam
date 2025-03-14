using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sociam.Application.Bases;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;
using System.Net;

namespace Sociam.Services.Services;
public sealed class FileService : IFileService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;
    private readonly Cloudinary _cloudinary;

    public FileService(
        IHttpContextAccessor contextAccessor,
        IConfiguration configuration,
        IOptions<CloudinarySettings> cloudinaryOptions)
    {
        _contextAccessor = contextAccessor;
        _configuration = configuration;
        var cloudinarySettings = cloudinaryOptions.Value;

        var account = new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret);

        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
        _cloudinary.Api.Timeout = 120000;

    }

    public async Task<(bool uploaded, string? fileName)> UploadFileAsync(
        IFormFile? file, string folderPath)
    {
        if (file is null || file.Length == 0)
            return (false, null);

        var uniqueFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmssfff}_{file.FileName}";

        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", folderPath);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var filePathToCreate = Path.Combine(directoryPath, uniqueFileName);

        await using var fileStream = new FileStream(filePathToCreate, FileMode.CreateNew);
        await file.CopyToAsync(fileStream);

        return (true, uniqueFileName);
    }

    public async Task<IEnumerable<FileUploadResult>> UploadFilesParallelAsync(
      IEnumerable<IFormFile> files, string? folderName = null, CancellationToken cancellationToken = default)
    {
        var formFiles = files.ToList();

        if (formFiles.Count == 0)
            return [];

        var uploadResults = formFiles.Select(file =>
        {
            return Task.Run(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                try
                {
                    string directoryPath;
                    string locationPath;
                    if (!string.IsNullOrEmpty(folderName))
                    {
                        directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", folderName);
                        locationPath = $"/Uploads/{folderName}";
                    }
                    else
                    {
                        var isImage = FileFormats.AllowedImageFormats.Contains(fileExtension);
                        var isVideo = FileFormats.AllowedVideoFormats.Contains(fileExtension);
                        if (isImage)
                        {
                            directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Images");
                            locationPath = "Uploads/Images";
                        }
                        else if (isVideo)
                        {
                            directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Videos");
                            locationPath = "Uploads/Videos";
                        }
                        else
                        {
                            directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Other");
                            locationPath = "Uploads/Other";
                        }
                    }

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var uniqueFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmssfff}_{file.FileName}";
                    var fullPathToCreate = Path.Combine(directoryPath, uniqueFileName);

                    await using var fileStream = new FileStream(fullPathToCreate, FileMode.Create, FileAccess.ReadWrite);
                    await file.CopyToAsync(fileStream, cancellationToken);

                    return new FileUploadResult
                    {
                        OriginalFileName = file.FileName,
                        SavedFileName = uniqueFileName,
                        Size = file.Length,
                        Type = GetFileType(fileExtension),
                        Url = _contextAccessor.HttpContext!.Request.IsHttps ?
                            $"{_configuration["BaseApiUrl"]}/{locationPath}/{uniqueFileName}" :
                            $"{_configuration["FullbackUrl"]}/{locationPath}/{uniqueFileName}"
                    };
                }
                catch (Exception)
                {
                    return new FileUploadResult
                    {
                        OriginalFileName = file.FileName,
                        SavedFileName = string.Empty,
                        Size = 0,
                        Url = string.Empty,
                    };
                }
            }, cancellationToken);
        });


        return await Task.WhenAll(uploadResults);
    }

    public bool DeleteFileFromPath(string filePath, string locationFolder)
    {
        if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(locationFolder))
            return false;

        var resourcePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "Uploads",
            locationFolder,
            filePath);

        if (!File.Exists(resourcePath)) return false;

        File.Delete(resourcePath);

        return true;

    }

    public async Task<Result<CloudinaryUploadResult>> CloudinaryUploadSingleFileAsync(IFormFile file)
    {
        var isValid = ValidateFile(file);

        if (!isValid)
            return Result<CloudinaryUploadResult>.Failure(HttpStatusCode.BadRequest, "Invalid file format.");

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        var fileType = GetFileType(fileExtension);

        using var stream = file.OpenReadStream();
        const int chunkSize = 20 * 1024 * 1024;
        UploadResult uploadResult;

        switch (fileType)
        {
            case FileType.Image:
                var imageParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "images",
                    UseFilename = true,
                    UniqueFilename = true
                };

                uploadResult = file.Length > 10 * 1024 * 1024
                    ? await _cloudinary.UploadLargeAsync(imageParams, chunkSize, CancellationToken.None)
                    : await _cloudinary.UploadAsync(imageParams, CancellationToken.None);
                break;

            case FileType.Video:
                var videoParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "videos",
                    UseFilename = true,
                    UniqueFilename = true,
                    EagerAsync = true
                };

                uploadResult = uploadResult = file.Length > 10 * 1024 * 1024
                    ? await _cloudinary.UploadLargeAsync(videoParams, chunkSize, CancellationToken.None)
                    : await _cloudinary.UploadAsync(videoParams, CancellationToken.None);
                break;

            case FileType.Document:
            case FileType.Text:
            case FileType.Audio:
                var rawParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "other",
                    UseFilename = true,
                    UniqueFilename = true
                };

                uploadResult = file.Length > 10 * 1024 * 1024
                    ? await _cloudinary.UploadLargeAsync(rawParams, chunkSize, CancellationToken.None)
                    : await _cloudinary.UploadAsync(rawParams);

                break;

            default:
                return Result<CloudinaryUploadResult>.Failure(HttpStatusCode.BadRequest, "Unsupported file type");
        }

        if (uploadResult.Error != null)
            return Result<CloudinaryUploadResult>.Failure(HttpStatusCode.InternalServerError,
                $"Upload failed: {uploadResult.Error.Message}");

        return Result<CloudinaryUploadResult>.Success(
            new CloudinaryUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                AssetId = uploadResult.AssetId
            });
    }

    private static bool ValidateFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return false;

        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        var isAudio = FileFormats.AllowedAudioFormats.Contains(fileExtension);
        var isDocument = FileFormats.AllowedDocumentFormats.Contains(fileExtension);
        var isImage = FileFormats.AllowedImageFormats.Contains(fileExtension);
        var isText = FileFormats.AllowedTextFormats.Contains(fileExtension);
        var isVideo = FileFormats.AllowedVideoFormats.Contains(fileExtension);

        return isAudio || isDocument || isImage || isText || isVideo;
    }

    public async Task<Result<string>> CloudinaryUploadMultipleFilesAsync(IFormFileCollection files)
    {
        throw new NotImplementedException();
    }

    private static FileType GetFileType(string fileExtension)
    {
        if (string.IsNullOrEmpty(fileExtension))
            throw new ArgumentException("File extension cannot be null or empty", nameof(fileExtension));

        if (FileFormats.AllowedVideoFormats.Contains(fileExtension))
            return FileType.Video;

        if (FileFormats.AllowedImageFormats.Contains(fileExtension))
            return FileType.Image;

        if (FileFormats.AllowedDocumentFormats.Contains(fileExtension))
            return FileType.Document;

        if (FileFormats.AllowedTextFormats.Contains(fileExtension))
            return FileType.Text;

        if (FileFormats.AllowedAudioFormats.Contains(fileExtension))
            return FileType.Audio;

        throw new NotSupportedException($"File extension '{fileExtension}' is not supported");
    }

    public async Task<Result<object>> GetResourceAsync(string assetId)
    {
        var result = await _cloudinary.GetResourceByAssetIdAsync(assetId);
        if (result.Error != null)
            return Result<object>.Failure(HttpStatusCode.NotFound);
        return Result<object>.Success(result);
    }
}
