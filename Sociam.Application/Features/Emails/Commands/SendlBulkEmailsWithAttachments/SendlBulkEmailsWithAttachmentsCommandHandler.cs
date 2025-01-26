using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Features.Emails.Commands.SendlBulkEmailsWithAttachments;
public sealed class SendlBulkEmailsWithAttachmentsCommandHandler(IMailService mailService) :
    IRequestHandler<SendlBulkEmailsWithAttachmentsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendlBulkEmailsWithAttachmentsCommand request,
        CancellationToken cancellationToken) =>
        await mailService.SendBulkEmailsWithAttachmentsAsync(
            new EmailBulkWithAttachments()
            {
                Message = request.Message,
                Subject = request.Subject,
                ToReceipients = request.ToReceipients,
                Attachments = request.Attachments
            });
}