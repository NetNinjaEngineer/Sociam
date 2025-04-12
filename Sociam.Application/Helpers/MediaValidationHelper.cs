using Microsoft.AspNetCore.Http;

namespace Sociam.Application.Helpers;

public static class MediaValidationHelper
{
    public static bool BeValidMediaFiles(IFormFileCollection? mediaFiles)
    {
        if (mediaFiles == null || !mediaFiles.Any())
            return true;

        foreach (var file in mediaFiles)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            List<string> allowedExtensions = [
                    ..FileFormats.AllowedVideoFormats,
                    ..FileFormats.AllowedTextFormats,
                    ..FileFormats.AllowedImageFormats,
                    ..FileFormats.AllowedDocumentFormats,
                    ..FileFormats.AllowedAudioFormats];

            if (!allowedExtensions.Contains(fileExtension))
                return false;
        }

        return true;
    }
}
