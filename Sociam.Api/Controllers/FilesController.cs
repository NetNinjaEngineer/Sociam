using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/files")]
[ApiController]
public class FilesController(IFileService fileService) : ControllerBase
{
    [HttpPost("upload-multiple-files")]
    public async Task<IActionResult> UploadMultipleWithParallelAsync(
        string? folderName,
        [FromForm] List<IFormFile> files)
    {
        var results = await fileService.UploadFilesParallelAsync(files, folderName);
        return Ok(results);
    }
}
