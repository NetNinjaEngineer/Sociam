namespace Sociam.Application.Helpers;
public static class FileFormats
{
    public static readonly List<string> AllowedImageFormats = [".jpg", ".jpeg", ".png", ".gif"];
    public static readonly List<string> AllowedVideoFormats = [".mp4", ".avi", ".mov", ".mkv"];
    public static readonly List<string> AllowedAudioFormats = [".mp3", ".wav", ".aac", ".flac"];
    public static readonly List<string> AllowedDocumentFormats = [".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx"];
    public static readonly List<string> AllowedTextFormats = [".txt", ".rtf", ".html", ".xml", ".json"];
}
