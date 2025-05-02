using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using Sociam.Application.Bases;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Services.Services;
public sealed class MailService(IOptions<SmtpSettings> smtpSettingsOptions) : IMailService
{
    private readonly SmtpSettings _smtpSettings = smtpSettingsOptions.Value;

    public async Task<Result<bool>> SendEmailAsync(EmailMessage emailMessage)
    {
        var messageResult = CreateMimeMessage(emailMessage.To, emailMessage.Subject, emailMessage.Message);

        var isSent = await SendMailMessageAsync(messageResult.Value);

        return IsEmailSent(emailMessage.To, isSent.Value);
    }

    public async Task<Result<bool>> SendEmailWithAttachmentsAsync(EmailMessageWithAttachments emailMessage)
    {
        var messageResult = await CreateMimeMessage(
            emailMessage.To,
            emailMessage.Subject,
            emailMessage.Message,
            [.. emailMessage.Attachments]);

        var isSent = await SendMailMessageAsync(messageResult.Value);

        return IsEmailSent(emailMessage.To, isSent.Value);

    }

    public async Task<Result<bool>> SendBulkEmailsAsync(EmailBulk emailMessage)
    {
        var message = CreateMimeMessage(emailMessage.ToReceipients, emailMessage.Subject, emailMessage.Message);
        var isSent = await SendMailMessageAsync(message.Value);
        return IsEmailSent(emailMessage.ToReceipients, isSent.Value);
    }

    public async Task<Result<bool>> SendBulkEmailsWithAttachmentsAsync(EmailBulkWithAttachments emailMessage)
    {
        var message = await CreateMimeMessage(
            emailMessage.ToReceipients,
            emailMessage.Subject,
            emailMessage.Message,
            emailMessage.Attachments);

        var isSent = await SendMailMessageAsync(message.Value);
        return IsEmailSent(emailMessage.ToReceipients, isSent.Value);
    }

    private static Result<bool> IsEmailSent(string toEmail, bool isSent)
    {
        return isSent ?
            Result<bool>.Success(true, $"Email is sent to '{toEmail}' successfully.")
            : Result<bool>.Failure(HttpStatusCode.BadRequest, "Email not sent.");
    }

    private static Result<bool> IsEmailSent(List<string> toMultipleEmails, bool isSent)
    {
        return isSent ?
            Result<bool>.Success(true, $"Email is sent to [{string.Join(" | ", toMultipleEmails)}] successfully.")
            : Result<bool>.Failure(HttpStatusCode.BadRequest, "Email not sent.");
    }


    private Result<MimeMessage> CreateMimeMessage(string toEmail, string subject, string htmlMessage)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        bodyBuilder.HtmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Sociam</title>
</head>

<body style=""font-family: 'Helvetica', Arial, sans-serif; margin: 0; padding: 0; background-color: #121212; color: #ffffff;"">
    <div style=""max-width: 600px; margin: 40px auto; padding: 0; position: relative;"">
        <!-- Header with gradient background -->
        <div style=""background: linear-gradient(135deg, #6b48ff, #00ddeb); border-radius: 12px 12px 0 0; padding: 25px; text-align: center;"">
            <div style=""width: 80px; height: 80px; margin: 0 auto 10px; border-radius: 20px; background: #ffffff; display: flex; align-items: center; justify-content: center; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
                <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 200 200"" width=""50"" height=""50"">
                    <!-- Gradient Definitions -->
                    <defs>
                        <linearGradient id=""logoGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#6b48ff"" />
                            <stop offset=""100%"" stop-color=""#00ddeb"" />
                        </linearGradient>
                        <linearGradient id=""innerGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#00ddeb"" />
                            <stop offset=""100%"" stop-color=""#6b48ff"" />
                        </linearGradient>
                    </defs>

                    <!-- Main Circle Background -->
                    <circle cx=""100"" cy=""100"" r=""90"" fill=""url(#logoGradient)"" />

                    <!-- Inner Circle -->
                    <circle cx=""100"" cy=""100"" r=""75"" fill=""white"" />

                    <!-- Q Letter Stylized -->
                    <path d=""M100,35 
                       a65,65 0 1,1 0,130 
                       a65,65 0 1,1 0,-130 
                       M100,55 
                       a45,45 0 1,0 0,90 
                       a45,45 0 1,0 0,-90 
                       M140,140 
                       l15,15 
                       l-10,10 
                       l-15,-15 
                       Z"" fill=""url(#innerGradient)"" />

                    <!-- Connection Lines -->
                    <circle cx=""70"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""130"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""100"" cy=""140"" r=""8"" fill=""white"" />

                    <line x1=""70"" y1=""80"" x2=""130"" y2=""80"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""70"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""130"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                </svg>
            </div>
        </div>

        <!-- Content area -->
        <div style=""background: #1f1f1f; border-radius: 0 0 12px 12px; padding: 30px; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
            {htmlMessage}
        </div>

        <!-- Footer -->
        <div style=""text-align: center; padding: 20px 0; font-size: 14px; color: #cccccc;"">
            <p style=""margin-bottom: 15px;"">Connect with us on Sociam!</p>

            <div style=""margin: 15px 0;"">
                <a href=""mailto:me5260287@gmail.com"" style=""color: #8e6bff; text-decoration: none; margin: 0 10px;"">Contact Us</a>
            </div>

            <div style=""font-size: 12px; color: #cccccc; margin-top: 15px;"">
                © 2025 Sociam. All rights reserved.
            </div>
        </div>
    </div>
</body>
</html>";

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private Result<MimeMessage> CreateMimeMessage(List<string> toReceipients, string subject, string htmlMessage)
    {
        var mimeMessage = InitMessage(subject);

        var bodyBuilder = new BodyBuilder();

        if (toReceipients.Count > 0)
            foreach (var toEmail in toReceipients)
                mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));

        bodyBuilder.HtmlBody = htmlMessage;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private async Task<Result<MimeMessage>> CreateMimeMessage(List<string> toReceipients,
        string subject, string htmlMessage, List<IFormFile> attachments)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        if (toReceipients.Count > 0)
            foreach (var toEmail in toReceipients)
                mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));

        bodyBuilder.HtmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Sociam</title>
</head>

<body style=""font-family: 'Helvetica', Arial, sans-serif; margin: 0; padding: 0; background-color: #121212; color: #ffffff;"">
    <div style=""max-width: 600px; margin: 40px auto; padding: 0; position: relative;"">
        <!-- Header with gradient background -->
        <div style=""background: linear-gradient(135deg, #6b48ff, #00ddeb); border-radius: 12px 12px 0 0; padding: 25px; text-align: center;"">
            <div style=""width: 80px; height: 80px; margin: 0 auto 10px; border-radius: 20px; background: #ffffff; display: flex; align-items: center; justify-content: center; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
                <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 200 200"" width=""50"" height=""50"">
                    <!-- Gradient Definitions -->
                    <defs>
                        <linearGradient id=""logoGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#6b48ff"" />
                            <stop offset=""100%"" stop-color=""#00ddeb"" />
                        </linearGradient>
                        <linearGradient id=""innerGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#00ddeb"" />
                            <stop offset=""100%"" stop-color=""#6b48ff"" />
                        </linearGradient>
                    </defs>

                    <!-- Main Circle Background -->
                    <circle cx=""100"" cy=""100"" r=""90"" fill=""url(#logoGradient)"" />

                    <!-- Inner Circle -->
                    <circle cx=""100"" cy=""100"" r=""75"" fill=""white"" />

                    <!-- Q Letter Stylized -->
                    <path d=""M100,35 
                       a65,65 0 1,1 0,130 
                       a65,65 0 1,1 0,-130 
                       M100,55 
                       a45,45 0 1,0 0,90 
                       a45,45 0 1,0 0,-90 
                       M140,140 
                       l15,15 
                       l-10,10 
                       l-15,-15 
                       Z"" fill=""url(#innerGradient)"" />

                    <!-- Connection Lines -->
                    <circle cx=""70"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""130"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""100"" cy=""140"" r=""8"" fill=""white"" />

                    <line x1=""70"" y1=""80"" x2=""130"" y2=""80"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""70"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""130"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                </svg>
            </div>
        </div>

        <!-- Content area -->
        <div style=""background: #1f1f1f; border-radius: 0 0 12px 12px; padding: 30px; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
            {htmlMessage}
        </div>

        <!-- Footer -->
        <div style=""text-align: center; padding: 20px 0; font-size: 14px; color: #cccccc;"">
            <p style=""margin-bottom: 15px;"">Connect with us on Sociam!</p>

            <div style=""margin: 15px 0;"">
                <a href=""mailto:me5260287@gmail.com"" style=""color: #8e6bff; text-decoration: none; margin: 0 10px;"">Contact Us</a>
            </div>

            <div style=""font-size: 12px; color: #cccccc; margin-top: 15px;"">
                © 2025 Sociam. All rights reserved.
            </div>
        </div>
    </div>
</body>
</html>";


        if (attachments?.Count > 0)
        {
            foreach (var file in attachments)
            {
                var fileName = Path.GetFileName(file.FileName);
                await bodyBuilder.Attachments.AddAsync(fileName, file.OpenReadStream());
            }
        }

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private async Task<Result<MimeMessage>> CreateMimeMessage(string toEmail,
        string subject, string htmlMessage, List<IFormFile> attachments)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        mimeMessage.Subject = subject;
        bodyBuilder.HtmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Sociam</title>
</head>

<body style=""font-family: 'Helvetica', Arial, sans-serif; margin: 0; padding: 0; background-color: #121212; color: #ffffff;"">
    <div style=""max-width: 600px; margin: 40px auto; padding: 0; position: relative;"">
        <!-- Header with gradient background -->
        <div style=""background: linear-gradient(135deg, #6b48ff, #00ddeb); border-radius: 12px 12px 0 0; padding: 25px; text-align: center;"">
            <div style=""width: 80px; height: 80px; margin: 0 auto 10px; border-radius: 20px; background: #ffffff; display: flex; align-items: center; justify-content: center; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
                <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 200 200"" width=""50"" height=""50"">
                    <!-- Gradient Definitions -->
                    <defs>
                        <linearGradient id=""logoGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#6b48ff"" />
                            <stop offset=""100%"" stop-color=""#00ddeb"" />
                        </linearGradient>
                        <linearGradient id=""innerGradient"" x1=""0%"" y1=""0%"" x2=""100%"" y2=""100%"">
                            <stop offset=""0%"" stop-color=""#00ddeb"" />
                            <stop offset=""100%"" stop-color=""#6b48ff"" />
                        </linearGradient>
                    </defs>

                    <!-- Main Circle Background -->
                    <circle cx=""100"" cy=""100"" r=""90"" fill=""url(#logoGradient)"" />

                    <!-- Inner Circle -->
                    <circle cx=""100"" cy=""100"" r=""75"" fill=""white"" />

                    <!-- Q Letter Stylized -->
                    <path d=""M100,35 
                       a65,65 0 1,1 0,130 
                       a65,65 0 1,1 0,-130 
                       M100,55 
                       a45,45 0 1,0 0,90 
                       a45,45 0 1,0 0,-90 
                       M140,140 
                       l15,15 
                       l-10,10 
                       l-15,-15 
                       Z"" fill=""url(#innerGradient)"" />

                    <!-- Connection Lines -->
                    <circle cx=""70"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""130"" cy=""80"" r=""8"" fill=""white"" />
                    <circle cx=""100"" cy=""140"" r=""8"" fill=""white"" />

                    <line x1=""70"" y1=""80"" x2=""130"" y2=""80"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""70"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                    <line x1=""130"" y1=""80"" x2=""100"" y2=""140"" stroke=""white"" stroke-width=""4"" />
                </svg>
            </div>
        </div>

        <!-- Content area -->
        <div style=""background: #1f1f1f; border-radius: 0 0 12px 12px; padding: 30px; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);"">
            {htmlMessage}
        </div>

        <!-- Footer -->
        <div style=""text-align: center; padding: 20px 0; font-size: 14px; color: #cccccc;"">
            <p style=""margin-bottom: 15px;"">Connect with us on Sociam!</p>

            <div style=""margin: 15px 0;"">
                <a href=""mailto:me5260287@gmail.com"" style=""color: #8e6bff; text-decoration: none; margin: 0 10px;"">Contact Us</a>
            </div>

            <div style=""font-size: 12px; color: #cccccc; margin-top: 15px;"">
                © 2025 Sociam. All rights reserved.
            </div>
        </div>
    </div>
</body>
</html>";

        if (attachments.Count > 0)
        {
            foreach (var file in attachments)
            {
                var fileName = Path.GetFileName(file.FileName);
                await bodyBuilder.Attachments.AddAsync(fileName, file.OpenReadStream());
            }
        }

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private MimeMessage InitMessage(string subject)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.Subject = subject;
        mimeMessage.From.Add(new MailboxAddress(_smtpSettings.Gmail.SenderName, _smtpSettings.Gmail.SenderEmail));
        return mimeMessage;
    }

    private async Task<Result<bool>> SendMailMessageAsync(MimeMessage message)
    {
        using var emailClient = new SmtpClient();

        await emailClient.ConnectAsync(_smtpSettings.Gmail.Host,
            _smtpSettings.Gmail.Port, SecureSocketOptions.StartTls, CancellationToken.None);

        await emailClient.AuthenticateAsync(_smtpSettings.Gmail.SenderEmail,
            _smtpSettings.Gmail.Password, CancellationToken.None);

        await emailClient.SendAsync(message, CancellationToken.None);

        await emailClient.DisconnectAsync(true);

        return Result<bool>.Success(true);
    }
}
