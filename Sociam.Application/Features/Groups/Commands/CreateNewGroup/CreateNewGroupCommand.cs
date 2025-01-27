using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Groups.Commands.CreateNewGroup;
public sealed class CreateNewGroupCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IFormFile? GroupPictureUrl { get; set; }
    public GroupPrivacy GroupPrivacy { get; set; }
}