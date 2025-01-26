using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;
using System.Dynamic;

namespace Sociam.Application.Features.Messages.Queries.GetAll;
public sealed class GetAllQueryHandler(
    IMessageRepository messageRepository,
    DataShaper<Message> dataShaper) : IRequestHandler<GetAllQuery, Result<IEnumerable<ExpandoObject>>>
{
    public async Task<Result<IEnumerable<ExpandoObject>>> Handle(GetAllQuery request,
                                                     CancellationToken cancellationToken)
    {
        var messages = await messageRepository.GetAllAsync();
        var shapedMessages = dataShaper.ShapeData(messages!, request.Fields!);
        return Result<IEnumerable<ExpandoObject>>.Success(shapedMessages);
    }
}
