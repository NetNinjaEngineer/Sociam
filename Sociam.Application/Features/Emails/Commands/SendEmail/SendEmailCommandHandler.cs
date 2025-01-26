using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Features.Emails.Commands.SendEmail;
public sealed class SendEmailCommandHandler(IMailService mailService) : IRequestHandler<SendEmailCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendEmailCommand request, CancellationToken cancellationToken) =>
        await mailService.SendEmailAsync(new EmailMessage()
        { Message = request.Message, Subject = request.Subject, To = request.To });
}
