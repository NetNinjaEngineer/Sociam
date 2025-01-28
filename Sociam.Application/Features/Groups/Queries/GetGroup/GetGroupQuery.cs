using FluentValidation;
using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;

namespace Sociam.Application.Features.Groups.Queries.GetGroup;

public sealed class GetGroupQuery : IRequest<Result<GroupListDto>>
{
    public Guid GroupId { get; set; }
}

public sealed class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator()
    {
        RuleFor(p => p.GroupId)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
