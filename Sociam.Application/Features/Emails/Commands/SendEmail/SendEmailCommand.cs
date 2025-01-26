using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Emails.Commands.SendEmail;
public sealed class SendEmailCommand : IRequest<Result<bool>>
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
}
