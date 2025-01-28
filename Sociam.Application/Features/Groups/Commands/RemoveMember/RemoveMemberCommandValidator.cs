using FluentValidation;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.RemoveMember
{
    public sealed class RemoveMemberCommandValidator : AbstractValidator<RemoveMemberCommand>
    {
        private readonly ICurrentUser _currentUser;

        public RemoveMemberCommandValidator(ICurrentUser currentUser)
        {
            _currentUser = currentUser;

            RuleFor(c => c.MemberId).Must(CanNotRemoveYourself)
                .WithMessage(DomainErrors.Group.CanNotRemoveYourself);
        }

        private bool CanNotRemoveYourself(Guid memberId)
            => memberId.ToString() != _currentUser.Id;
    }
}
