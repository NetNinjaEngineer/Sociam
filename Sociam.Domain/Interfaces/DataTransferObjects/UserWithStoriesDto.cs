namespace Sociam.Domain.Interfaces.DataTransferObjects;
public sealed class UserWithStoriesDto
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string? ProfilePicture { get; set; }
    public List<StoryForReturnDto> Stories { get; set; } = [];
}