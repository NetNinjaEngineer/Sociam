using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Groups.Commands.CreateNewGroup;
public sealed class CreateNewGroupCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IFormFile? GroupPictureUrl { get; set; }
}