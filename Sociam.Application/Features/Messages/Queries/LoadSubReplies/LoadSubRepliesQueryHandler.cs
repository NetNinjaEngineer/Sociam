using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.LoadSubReplies;
public sealed class LoadSubRepliesQueryHandler(IMessageService service) :
    IRequestHandler<LoadSubRepliesQuery, Result<IReadOnlyList<MessageReplyDto>>>
{
    public async Task<Result<IReadOnlyList<MessageReplyDto>>> Handle(LoadSubRepliesQuery request, CancellationToken cancellationToken)
    {
        return await service.RetrieveChildRepliesAsync(request);
    }
}
