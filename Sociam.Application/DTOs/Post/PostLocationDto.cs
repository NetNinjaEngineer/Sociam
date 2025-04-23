using Sociam.Domain.Entities;

namespace Sociam.Application.DTOs.Post;

public sealed class PostLocationDto
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}