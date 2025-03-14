using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/files")]
[ApiController]
public class FilesController(IFileService service) : ControllerBase
{
    [HttpPost("cloudinary/upload")]
    public async Task<IActionResult> UploadFileToCloudinaryAsync(IFormFile file)
    {
        var result = await service.CloudinaryUploadSingleFileAsync(file);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPost("cloudinary/upload-multiple")]
    public async Task<IActionResult> UploadMultipleFilesToCloudinaryAsync([FromForm] IFormFileCollection files)
    {
        var result = await service.CloudinaryUploadMultipleFilesAsync(files);
        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPost("server/upload-multiple")]
    public async Task<IActionResult> UploadFilesToServerAsync([FromQuery] string? folderName, [FromForm] List<IFormFile> files)
    {
        var result = await service.UploadFilesParallelAsync(files, folderName);
        return Ok(result);
    }

    [HttpGet("get-resource/{assetId}")]
    public async Task<IActionResult> GetCloudinaryResourceAsync(string assetId)
    {
        var result = await service.GetResourceAsync(assetId);
        return Ok(result);
    }
}
