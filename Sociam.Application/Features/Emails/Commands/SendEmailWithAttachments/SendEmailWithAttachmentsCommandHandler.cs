using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Features.Emails.Commands.SendEmailWithAttachments;
public sealed class SendEmailWithAttachmentsCommandHandler(IMailService mailService) :
    IRequestHandler<SendEmailWithAttachmentsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendEmailWithAttachmentsCommand request,
        CancellationToken cancellationToken) =>
        await mailService.SendEmailWithAttachmentsAsync(
            new EmailMessageWithAttachments()
            {
                To = request.To,
                Message = request.Message,
                Subject = request.Subject,
                Attachments = request.Attachments
            });
}
