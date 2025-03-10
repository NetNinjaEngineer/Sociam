using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountPassword;
public sealed class ChangeAccountPasswordCommandHandler(IUserService service) :
    IRequestHandler<ChangeAccountPasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        return await service.ChangeAccountPasswordAsync(request);
    }
}
