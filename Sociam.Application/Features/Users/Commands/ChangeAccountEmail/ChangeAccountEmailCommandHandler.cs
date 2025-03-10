using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountEmail;
public sealed class ChangeAccountEmailCommandHandler(IUserService service) : IRequestHandler<ChangeAccountEmailCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ChangeAccountEmailCommand request, CancellationToken cancellationToken)
    {
        return await service.ChangeAccountEmailAsync(request);
    }
}
