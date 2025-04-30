using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Queries.GetUsernameSuggestions;
public sealed class GetUsernameSuggestionsQueryHandler(IUserService service)
    : IRequestHandler<GetUsernameSuggestionsQuery, Result<List<string>>>
{
    public async Task<Result<List<string>>> Handle(
        GetUsernameSuggestionsQuery request, CancellationToken cancellationToken)
        => await service.GetUsernameSuggestionsAsync(request);
}
