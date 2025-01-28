namespace Sociam.Application.DTOs.Groups
{
    public class GroupListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
    }
}
