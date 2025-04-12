using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sociam.Application.Bases;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

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

        _cloudinary = new Cloudinary(
            new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret))
        {
            Api = { Secure = true, Timeout = 300000 }
        };
    }


    public async Task<(bool uploaded, string? fileName)> UploadFileAsync(IFormFile? file, string folderPath)
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

    public async Task<IEnumerable<FileUploadResult>> UploadFilesParallelAsync(IEnumerable<IFormFile> files, string? folderName = null, CancellationToken cancellationToken = default)
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
        if (file.Length == 0)
            return Result<CloudinaryUploadResult>.Failure(HttpStatusCode.BadRequest, "Invalid or empty file.");

        var fileType = GetFileType(Path.GetExtension(file.FileName).ToLower());
        var folder = fileType switch
        {
            FileType.Image => "images",
            FileType.Video => "videos",
            _ => "other"
        };

        await using var stream = file.OpenReadStream();
        var uploadParams = fileType switch
        {
            FileType.Image => new ImageUploadParams { File = new FileDescription(file.FileName, stream), Folder = folder, UseFilename = true, UniqueFilename = true },
            FileType.Video => new VideoUploadParams { File = new FileDescription(file.FileName, stream), Folder = folder, UseFilename = true, UniqueFilename = true, EagerAsync = true },
            _ => new RawUploadParams { File = new FileDescription(file.FileName, stream), Folder = folder, UseFilename = true, UniqueFilename = true }
        };

        var uploadResult = file.Length > 10 * 1024 * 1024
            ? await _cloudinary.UploadLargeAsync(uploadParams, 20 * 1024 * 1024)
            : await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            return Result<CloudinaryUploadResult>.Failure(HttpStatusCode.InternalServerError, $"Upload failed: {uploadResult.Error.Message}");

        return Result<CloudinaryUploadResult>.Success(new CloudinaryUploadResult
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.ToString(),
            AssetId = uploadResult.AssetId,
            Type = fileType
        });
    }

    public async Task<Result<List<CloudinaryUploadResult>>> CloudinaryUploadMultipleFilesAsync(IFormFileCollection files)
    {
        if (files.Count == 0)
            return Result<List<CloudinaryUploadResult>>.Failure(HttpStatusCode.BadRequest, "No files to upload.");

        var uploadTasks = files.Select(CloudinaryUploadSingleFileAsync);
        var results = await Task.WhenAll(uploadTasks);

        return Result<List<CloudinaryUploadResult>>.Success(results.Where(r => r.IsSuccess).Select(r => r.Value).ToList());
    }

    public async Task<Result<object>> GetResourceAsync(string assetId)
    {
        var result = await _cloudinary.GetResourceByAssetIdAsync(assetId);
        return result.Error != null ?
            Result<object>.Failure(HttpStatusCode.NotFound) : Result<object>.Success(result);
    }

    public async Task<Result<bool>> DeleteCloudinaryResourceAsync(string publicId, FileType fileType)
    {
        var deleteParams = fileType switch
        {
            FileType.Image => new DeletionParams(publicId) { ResourceType = ResourceType.Image },
            FileType.Video => new DeletionParams(publicId) { ResourceType = ResourceType.Video },
            _ => new DeletionParams(publicId) { ResourceType = ResourceType.Raw }
        };

        var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
        return deleteResult.StatusCode == HttpStatusCode.OK ?
            Result<bool>.Success(true) : Result<bool>.Failure(HttpStatusCode.BadRequest, deleteResult.Error.Message);
    }

    private static FileType GetFileType(string fileExtension)
    {
        return FileFormats.AllowedImageFormats.Contains(fileExtension) ? FileType.Image
             : FileFormats.AllowedVideoFormats.Contains(fileExtension) ? FileType.Video
             : FileFormats.AllowedDocumentFormats.Contains(fileExtension) ? FileType.Document
             : FileFormats.AllowedTextFormats.Contains(fileExtension) ? FileType.Text
             : FileFormats.AllowedAudioFormats.Contains(fileExtension) ? FileType.Audio
             : throw new NotSupportedException($"Unsupported file type: {fileExtension}");
    }
}