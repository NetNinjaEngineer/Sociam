using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.VerifyChangeEmail;
public sealed class VerifyChangeEmailCommandHandler(IUserService service) : IRequestHandler<VerifyChangeEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        VerifyChangeEmailCommand request, CancellationToken cancellationToken)
    {
        return await service.VerifyChangeAccountEmailAsync(request);
    }
}
