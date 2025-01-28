using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Groups.Commands.HandleJoinRequest
{
    public sealed class HandleJoinRequestCommand : IRequest<Result<string>>
    {
        public Guid RequestId { get; set; }
        public Guid GroupId { get; set; }
        public JoinRequestStatus JoinStatus { get; set; }
    }
}
