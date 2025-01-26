using MediatR;
using Sociam.Application.Bases;
using System.Dynamic;

namespace Sociam.Application.Features.Messages.Queries.GetAll;
public sealed class GetAllQuery : IRequest<Result<IEnumerable<ExpandoObject>>>
{
    public string? Fields { get; set; }
}
