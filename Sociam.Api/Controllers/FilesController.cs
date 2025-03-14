using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/files")]
[ApiController]
public class FilesController(IFileService service) : ControllerBase
{
    [Route("cloudinary/upload")]
    [HttpPost]
    public async Task<IActionResult> CloudinaryUploadFileAsync(IFormFile file)
    {
        var result = await service.CloudinaryUploadSingleFileAsync(file);
        return Ok(result);
    }

    [Route("server/upload-multiple")]
    [HttpPost]
    public async Task<IActionResult> UploadMultipleWithParallelAsync(
        string? folderName, [FromForm] List<IFormFile> files)
    {
        var result = await service.UploadFilesParallelAsync(files, folderName);
        return Ok(result);
    }

    [HttpGet("get-resource")]
    public async Task<IActionResult> GetResourceAsync(string assetId)
    {
        var results = await service.GetResourceAsync(assetId);
        return Ok(results);
    }
}
