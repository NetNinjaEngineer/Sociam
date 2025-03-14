namespace Sociam.Application.Interfaces.Services.Models;
public sealed class CloudinaryUploadResult
{
    public string AssetId { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
