using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Interfaces.Services;
public interface IMailService
{
    Task<Result<bool>> SendEmailAsync(EmailMessage emailMessage);
    Task<Result<bool>> SendEmailWithAttachmentsAsync(EmailMessageWithAttachments emailMessage);
    Task<Result<bool>> SendBulkEmailsAsync(EmailBulk emailMessage);
    Task<Result<bool>> SendBulkEmailsWithAttachmentsAsync(EmailBulkWithAttachments emailMessage);
}
