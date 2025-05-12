﻿using System.Net;
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


    private Result<MimeMessage> CreateMimeMessage(string toEmail, string subject, string textBody)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        bodyBuilder.TextBody = textBody;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private Result<MimeMessage> CreateMimeMessage(List<string> toReceipients, string subject, string textBody)
    {
        var mimeMessage = InitMessage(subject);

        var bodyBuilder = new BodyBuilder();

        if (toReceipients.Count > 0)
            foreach (var toEmail in toReceipients)
                mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));

        bodyBuilder.TextBody = textBody;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return Result<MimeMessage>.Success(mimeMessage);
    }

    private async Task<Result<MimeMessage>> CreateMimeMessage(List<string> toReceipients,
        string subject, string textBody, List<IFormFile> attachments)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        if (toReceipients.Count > 0)
            foreach (var toEmail in toReceipients)
                mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));

        bodyBuilder.TextBody = textBody;

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

    private async Task<Result<MimeMessage>> CreateMimeMessage(string toEmail,
        string subject, string textBody, List<IFormFile> attachments)
    {
        var mimeMessage = InitMessage(subject);
        var bodyBuilder = new BodyBuilder();

        mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        mimeMessage.Subject = subject;

        bodyBuilder.TextBody = textBody;

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
