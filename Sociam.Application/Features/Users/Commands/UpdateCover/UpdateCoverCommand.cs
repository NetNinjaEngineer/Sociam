using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Commands.UpdateCover;
public sealed class UpdateCoverCommand : IRequest<Result<string>>
{
    public IFormFile Cover { get; set; } = null!;
}