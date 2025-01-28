using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Groups.Commands.RemoveMember
{
    public sealed class RemoveMemberCommand : IRequest<Result<bool>>
    {
        public Guid GroupId { get; set; }
        public Guid MemberId { get; set; }
    }
}
