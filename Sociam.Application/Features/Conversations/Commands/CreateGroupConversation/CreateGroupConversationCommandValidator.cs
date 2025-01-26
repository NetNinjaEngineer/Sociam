using FluentValidation;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;

public sealed class CreateGroupConversationCommandValidator : AbstractValidator<CreateGroupConversationCommand>
{
    public CreateGroupConversationCommandValidator(IUnitOfWork unitOfWork) =>
        RuleFor(c => c.GroupId)
            .MustAsync(async (groupId, cancellationToken)
                => await unitOfWork.Repository<Group>()?.GetByIdAsync(groupId)! != null);
}