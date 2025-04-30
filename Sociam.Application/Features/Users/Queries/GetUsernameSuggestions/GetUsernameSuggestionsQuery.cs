using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Queries.GetUsernameSuggestions;
public sealed class GetUsernameSuggestionsQuery : IRequest<Result<List<string>>>
{
    public string Username { get; set; } = string.Empty;
}
