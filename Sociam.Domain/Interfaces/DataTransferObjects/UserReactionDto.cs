namespace Sociam.Domain.Interfaces.DataTransferObjects;
public sealed class UserReactionDto
{
    public string User { get; set; } = string.Empty;
    public List<string> Reactions { get; set; } = [];
}
