using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Interfaces;

namespace Sociam.Application.Features.Groups.Commands.AddUserToGroup;

public sealed class AddUserToGroupCommandValidator : AbstractValidator<AddUserToGroupCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public AddUserToGroupCommandValidator(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;


        RuleFor(c => c.GroupId)
            .MustAsync(MustBeAValidGroup)
            .WithMessage("The provided GroupId '{PropertyValue}' is invalid or does not exist.");

        RuleFor(c => c.UserId)
            .MustAsync(MustBeAValidUser)
            .WithMessage("The provided UserId '{PropertyValue}' is invalid or does not exist.");
    }

    private async Task<bool> MustBeAValidUser(Guid id, CancellationToken token) => await _userManager.FindByIdAsync(id.ToString()) != null;

    private async Task<bool> MustBeAValidGroup(Guid id, CancellationToken token) => await _unitOfWork.Repository<Group>()!.GetByIdAsync(id) != null;
}
