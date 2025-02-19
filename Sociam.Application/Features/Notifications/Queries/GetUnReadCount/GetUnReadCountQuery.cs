using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Queries.GetUnReadCount;
public sealed class GetUnReadCountQuery : IRequest<Result<long>>
{
}
