using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Queries.GetReadCount;
public sealed class GetReadCountQuery : IRequest<Result<long>>
{
}
