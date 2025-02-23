﻿using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;

namespace Sociam.Application.Features.Emails.Commands.SendBulkEmails;
public sealed class SendBulkEmailsCommandHandler(IMailService mailService) : IRequestHandler<SendBulkEmailsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendBulkEmailsCommand request, CancellationToken cancellationToken) =>
        await mailService.SendBulkEmailsAsync(new EmailBulk()
        {
            Message = request.Message,
            Subject = request.Subject,
            ToReceipients = request.ToRecepients
        });
}
