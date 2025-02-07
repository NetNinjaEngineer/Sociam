using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.AddStoryComment;
public sealed class AddStoryCommentCommandHandler(IStoryService service)
    : IRequestHandler<AddStoryCommentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddStoryCommentCommand request, CancellationToken cancellationToken)
        => await service.CommentToStoryAsync(request);
}
