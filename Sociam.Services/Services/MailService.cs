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
	<style>
		@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');

		:root {{
			--primary-color: #6b48ff;
			--secondary-color: #00ddeb;
			--text-primary: #333333;
			--text-secondary: #666666;
			--bg-primary: #ffffff;
			--bg-secondary: #f0f2f5;
			--bg-accent: #e8e9ff;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
			--border-radius: 12px;
		}}

		/* Dark mode */
		.dark-mode {{
			--primary-color: #8e6bff;
			--secondary-color: #33e6ff;
			--text-primary: #ffffff;
			--text-secondary: #cccccc;
			--bg-primary: #1f1f1f;
			--bg-secondary: #121212;
			--bg-accent: #2d2a40;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
		}}

		body {{
			font-family: 'Poppins', sans-serif;
			margin: 0;
			padding: 0;
			background-color: var(--bg-secondary);
			color: var(--text-primary);
			transition: all 0.3s ease;
		}}

		.mode-toggle {{
			position: absolute;
			top: 20px;
			right: 20px;
			cursor: pointer;
			background: var(--bg-primary);
			border: 1px solid var(--text-secondary);
			border-radius: 20px;
			padding: 5px 10px;
			font-size: 12px;
			color: var(--text-secondary);
			z-index: 10;
			transition: all 0.3s ease;
		}}

		.mode-toggle:hover {{
			background: var(--bg-accent);
		}}

		.container {{
			max-width: 600px;
			margin: 40px auto;
			padding: 0;
			position: relative;
			animation: fadeIn 0.8s ease-in;
		}}

		@keyframes fadeIn {{
			from {{
				opacity: 0;
				transform: translateY(20px);
			}}

			to {{
				opacity: 1;
				transform: translateY(0);
			}}
		}}

		.header {{
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			border-radius: var(--border-radius) var(--border-radius) 0 0;
			padding: 25px;
			text-align: center;
		}}

		.logo {{
			width: 80px;
			height: 80px;
			margin-bottom: 10px;
			border-radius: 20px;
			background: var(--bg-primary);
			display: flex;
			align-items: center;
			justify-content: center;
			box-shadow: var(--shadow);
			margin: 0 auto;
		}}

		.logo-text {{
			font-weight: 700;
			font-size: 24px;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
		}}

		.content {{
			background: var(--bg-primary);
			border-radius: 0 0 var(--border-radius) var(--border-radius);
			padding: 30px;
			box-shadow: var(--shadow);
		}}

		h1 {{
			color: var(--primary-color);
			font-size: 24px;
			margin: 0 0 20px;
			font-weight: 600;
		}}

		p {{
			font-size: 16px;
			line-height: 1.6;
			color: var(--text-secondary);
			margin: 0 0 20px;
		}}

		.code-container {{
			text-align: center;
			margin: 30px 0;
		}}

		.code {{
			background: var(--bg-accent);
			display: inline-block;
			padding: 15px 30px;
			font-size: 32px;
			font-weight: 600;
			color: var(--primary-color);
			border-radius: 12px;
			letter-spacing: 2px;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.15);
		}}

		.timer {{
			display: block;
			margin-top: 10px;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.btn {{
			display: block;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			color: white;
			text-decoration: none;
			padding: 12px 25px;
			border-radius: 30px;
			font-weight: 600;
			text-align: center;
			margin: 30px auto;
			max-width: 200px;
			transition: all 0.3s ease;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.25);
		}}

		.btn:hover {{
			transform: translateY(-2px);
			box-shadow: 0 6px 15px rgba(107, 72, 255, 0.3);
		}}

		.divider {{
			height: 1px;
			background: var(--bg-accent);
			margin: 25px 0;
		}}

		.signature {{
			font-weight: 500;
			color: var(--text-primary);
		}}

		.team {{
			font-weight: 400;
			color: var(--primary-color);
		}}

		.footer {{
			text-align: center;
			padding: 20px 0;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.social-links {{
			display: flex;
			justify-content: center;
			gap: 15px;
			margin: 15px 0;
		}}

		.social-icon {{
			width: 36px;
			height: 36px;
			border-radius: 50%;
			background: var(--bg-accent);
			display: flex;
			align-items: center;
			justify-content: center;
			transition: all 0.3s ease;
		}}

		.social-icon:hover {{
			transform: translateY(-3px);
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
		}}

		.social-icon svg {{
			width: 18px;
			height: 18px;
			fill: var(--primary-color);
		}}

		.social-icon:hover svg {{
			fill: white;
		}}

		.footer-links {{
			margin: 15px 0;
		}}

		.footer-links a {{
			color: var(--primary-color);
			text-decoration: none;
			margin: 0 10px;
			transition: all 0.3s ease;
		}}

		.footer-links a:hover {{
			color: var(--secondary-color);
		}}

		.copyright {{
			font-size: 12px;
			color: var(--text-secondary);
			margin-top: 15px;
		}}

		@media (max-width: 650px) {{
			.container {{
				margin: 20px 15px;
			}}

			.header {{
				padding: 20px;
			}}

			.content {{
				padding: 25px 20px;
			}}

			h1 {{
				font-size: 20px;
			}}

			p {{
				font-size: 14px;
			}}

			.code {{
				font-size: 26px;
				padding: 12px 20px;
			}}
		}}
	</style>
</head>

<body class=""dark-mode"">
	<div class=""container"">
		<div class=""header"">
			<div class=""logo"">
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
						<filter id=""glow"" x=""-20%"" y=""-20%"" width=""140%"" height=""140%"">
							<feGaussianBlur stdDeviation=""2"" result=""blur"" />
							<feComposite in=""SourceGraphic"" in2=""blur"" operator=""over"" />
						</filter>
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
					   Z"" fill=""url(#innerGradient)"" filter=""url(#glow)"" />

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

		<div class=""content"">
			{htmlMessage}
		</div>

		<div class=""footer"">
			<p>Connect with us on Sociam!</p>

			<div class=""footer-links"">
				<a href=""mailto:me5260287@gmail.com"">Contact Us</a>
			</div>

			<div class=""copyright"">
				© 2025 Sociam. All rights reserved.
			</div>
		</div>
	</div>

	<script>

		// Countdown timer simulation
		let timeLeft = 180;
		const timerElement = document.querySelector('.timer');

		function updateTimer() {{
			const minutes = Math.floor(timeLeft / 60);
			const seconds = timeLeft % 60;
			timerElement.textContent = `This code will expire in ${{minutes}}:${{seconds < 10 ? '0' : ''}}${{seconds}} minutes`;

			if (timeLeft > 0) {{
				timeLeft--;
				setTimeout(updateTimer, 1000);
			}} else {{
				timerElement.textContent = 'This code has expired';
				timerElement.style.color = '#ff3b30';
			}}
		}}

		updateTimer();
	</script>
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
	<style>
		@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');

		:root {{
			--primary-color: #6b48ff;
			--secondary-color: #00ddeb;
			--text-primary: #333333;
			--text-secondary: #666666;
			--bg-primary: #ffffff;
			--bg-secondary: #f0f2f5;
			--bg-accent: #e8e9ff;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
			--border-radius: 12px;
		}}

		/* Dark mode */
		.dark-mode {{
			--primary-color: #8e6bff;
			--secondary-color: #33e6ff;
			--text-primary: #ffffff;
			--text-secondary: #cccccc;
			--bg-primary: #1f1f1f;
			--bg-secondary: #121212;
			--bg-accent: #2d2a40;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
		}}

		body {{
			font-family: 'Poppins', sans-serif;
			margin: 0;
			padding: 0;
			background-color: var(--bg-secondary);
			color: var(--text-primary);
			transition: all 0.3s ease;
		}}

		.mode-toggle {{
			position: absolute;
			top: 20px;
			right: 20px;
			cursor: pointer;
			background: var(--bg-primary);
			border: 1px solid var(--text-secondary);
			border-radius: 20px;
			padding: 5px 10px;
			font-size: 12px;
			color: var(--text-secondary);
			z-index: 10;
			transition: all 0.3s ease;
		}}

		.mode-toggle:hover {{
			background: var(--bg-accent);
		}}

		.container {{
			max-width: 600px;
			margin: 40px auto;
			padding: 0;
			position: relative;
			animation: fadeIn 0.8s ease-in;
		}}

		@keyframes fadeIn {{
			from {{
				opacity: 0;
				transform: translateY(20px);
			}}

			to {{
				opacity: 1;
				transform: translateY(0);
			}}
		}}

		.header {{
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			border-radius: var(--border-radius) var(--border-radius) 0 0;
			padding: 25px;
			text-align: center;
		}}

		.logo {{
			width: 80px;
			height: 80px;
			margin-bottom: 10px;
			border-radius: 20px;
			background: var(--bg-primary);
			display: flex;
			align-items: center;
			justify-content: center;
			box-shadow: var(--shadow);
			margin: 0 auto;
		}}

		.logo-text {{
			font-weight: 700;
			font-size: 24px;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
		}}

		.content {{
			background: var(--bg-primary);
			border-radius: 0 0 var(--border-radius) var(--border-radius);
			padding: 30px;
			box-shadow: var(--shadow);
		}}

		h1 {{
			color: var(--primary-color);
			font-size: 24px;
			margin: 0 0 20px;
			font-weight: 600;
		}}

		p {{
			font-size: 16px;
			line-height: 1.6;
			color: var(--text-secondary);
			margin: 0 0 20px;
		}}

		.code-container {{
			text-align: center;
			margin: 30px 0;
		}}

		.code {{
			background: var(--bg-accent);
			display: inline-block;
			padding: 15px 30px;
			font-size: 32px;
			font-weight: 600;
			color: var(--primary-color);
			border-radius: 12px;
			letter-spacing: 2px;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.15);
		}}

		.timer {{
			display: block;
			margin-top: 10px;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.btn {{
			display: block;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			color: white;
			text-decoration: none;
			padding: 12px 25px;
			border-radius: 30px;
			font-weight: 600;
			text-align: center;
			margin: 30px auto;
			max-width: 200px;
			transition: all 0.3s ease;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.25);
		}}

		.btn:hover {{
			transform: translateY(-2px);
			box-shadow: 0 6px 15px rgba(107, 72, 255, 0.3);
		}}

		.divider {{
			height: 1px;
			background: var(--bg-accent);
			margin: 25px 0;
		}}

		.signature {{
			font-weight: 500;
			color: var(--text-primary);
		}}

		.team {{
			font-weight: 400;
			color: var(--primary-color);
		}}

		.footer {{
			text-align: center;
			padding: 20px 0;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.social-links {{
			display: flex;
			justify-content: center;
			gap: 15px;
			margin: 15px 0;
		}}

		.social-icon {{
			width: 36px;
			height: 36px;
			border-radius: 50%;
			background: var(--bg-accent);
			display: flex;
			align-items: center;
			justify-content: center;
			transition: all 0.3s ease;
		}}

		.social-icon:hover {{
			transform: translateY(-3px);
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
		}}

		.social-icon svg {{
			width: 18px;
			height: 18px;
			fill: var(--primary-color);
		}}

		.social-icon:hover svg {{
			fill: white;
		}}

		.footer-links {{
			margin: 15px 0;
		}}

		.footer-links a {{
			color: var(--primary-color);
			text-decoration: none;
			margin: 0 10px;
			transition: all 0.3s ease;
		}}

		.footer-links a:hover {{
			color: var(--secondary-color);
		}}

		.copyright {{
			font-size: 12px;
			color: var(--text-secondary);
			margin-top: 15px;
		}}

		@media (max-width: 650px) {{
			.container {{
				margin: 20px 15px;
			}}

			.header {{
				padding: 20px;
			}}

			.content {{
				padding: 25px 20px;
			}}

			h1 {{
				font-size: 20px;
			}}

			p {{
				font-size: 14px;
			}}

			.code {{
				font-size: 26px;
				padding: 12px 20px;
			}}
		}}
	</style>
</head>

<body class=""dark-mode"">
	<div class=""container"">
		<div class=""header"">
			<div class=""logo"">
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
						<filter id=""glow"" x=""-20%"" y=""-20%"" width=""140%"" height=""140%"">
							<feGaussianBlur stdDeviation=""2"" result=""blur"" />
							<feComposite in=""SourceGraphic"" in2=""blur"" operator=""over"" />
						</filter>
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
					   Z"" fill=""url(#innerGradient)"" filter=""url(#glow)"" />

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

		<div class=""content"">
			{htmlMessage}
		</div>

		<div class=""footer"">
			<p>Connect with us on Sociam!</p>

			<div class=""footer-links"">
				<a href=""mailto:me5260287@gmail.com"">Contact Us</a>
			</div>

			<div class=""copyright"">
				© 2025 Sociam. All rights reserved.
			</div>
		</div>
	</div>

	<script>

		// Countdown timer simulation
		let timeLeft = 180;
		const timerElement = document.querySelector('.timer');

		function updateTimer() {{
			const minutes = Math.floor(timeLeft / 60);
			const seconds = timeLeft % 60;
			timerElement.textContent = `This code will expire in ${{minutes}}:${{seconds < 10 ? '0' : ''}}${{seconds}} minutes`;

			if (timeLeft > 0) {{
				timeLeft--;
				setTimeout(updateTimer, 1000);
			}} else {{
				timerElement.textContent = 'This code has expired';
				timerElement.style.color = '#ff3b30';
			}}
		}}

		updateTimer();
	</script>
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
	<style>
		@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');

		:root {{
			--primary-color: #6b48ff;
			--secondary-color: #00ddeb;
			--text-primary: #333333;
			--text-secondary: #666666;
			--bg-primary: #ffffff;
			--bg-secondary: #f0f2f5;
			--bg-accent: #e8e9ff;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
			--border-radius: 12px;
		}}

		/* Dark mode */
		.dark-mode {{
			--primary-color: #8e6bff;
			--secondary-color: #33e6ff;
			--text-primary: #ffffff;
			--text-secondary: #cccccc;
			--bg-primary: #1f1f1f;
			--bg-secondary: #121212;
			--bg-accent: #2d2a40;
			--shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
		}}

		body {{
			font-family: 'Poppins', sans-serif;
			margin: 0;
			padding: 0;
			background-color: var(--bg-secondary);
			color: var(--text-primary);
			transition: all 0.3s ease;
		}}

		.mode-toggle {{
			position: absolute;
			top: 20px;
			right: 20px;
			cursor: pointer;
			background: var(--bg-primary);
			border: 1px solid var(--text-secondary);
			border-radius: 20px;
			padding: 5px 10px;
			font-size: 12px;
			color: var(--text-secondary);
			z-index: 10;
			transition: all 0.3s ease;
		}}

		.mode-toggle:hover {{
			background: var(--bg-accent);
		}}

		.container {{
			max-width: 600px;
			margin: 40px auto;
			padding: 0;
			position: relative;
			animation: fadeIn 0.8s ease-in;
		}}

		@keyframes fadeIn {{
			from {{
				opacity: 0;
				transform: translateY(20px);
			}}

			to {{
				opacity: 1;
				transform: translateY(0);
			}}
		}}

		.header {{
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			border-radius: var(--border-radius) var(--border-radius) 0 0;
			padding: 25px;
			text-align: center;
		}}

		.logo {{
			width: 80px;
			height: 80px;
			margin-bottom: 10px;
			border-radius: 20px;
			background: var(--bg-primary);
			display: flex;
			align-items: center;
			justify-content: center;
			box-shadow: var(--shadow);
			margin: 0 auto;
		}}

		.logo-text {{
			font-weight: 700;
			font-size: 24px;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
		}}

		.content {{
			background: var(--bg-primary);
			border-radius: 0 0 var(--border-radius) var(--border-radius);
			padding: 30px;
			box-shadow: var(--shadow);
		}}

		h1 {{
			color: var(--primary-color);
			font-size: 24px;
			margin: 0 0 20px;
			font-weight: 600;
		}}

		p {{
			font-size: 16px;
			line-height: 1.6;
			color: var(--text-secondary);
			margin: 0 0 20px;
		}}

		.code-container {{
			text-align: center;
			margin: 30px 0;
		}}

		.code {{
			background: var(--bg-accent);
			display: inline-block;
			padding: 15px 30px;
			font-size: 32px;
			font-weight: 600;
			color: var(--primary-color);
			border-radius: 12px;
			letter-spacing: 2px;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.15);
		}}

		.timer {{
			display: block;
			margin-top: 10px;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.btn {{
			display: block;
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
			color: white;
			text-decoration: none;
			padding: 12px 25px;
			border-radius: 30px;
			font-weight: 600;
			text-align: center;
			margin: 30px auto;
			max-width: 200px;
			transition: all 0.3s ease;
			box-shadow: 0 4px 10px rgba(107, 72, 255, 0.25);
		}}

		.btn:hover {{
			transform: translateY(-2px);
			box-shadow: 0 6px 15px rgba(107, 72, 255, 0.3);
		}}

		.divider {{
			height: 1px;
			background: var(--bg-accent);
			margin: 25px 0;
		}}

		.signature {{
			font-weight: 500;
			color: var(--text-primary);
		}}

		.team {{
			font-weight: 400;
			color: var(--primary-color);
		}}

		.footer {{
			text-align: center;
			padding: 20px 0;
			font-size: 14px;
			color: var(--text-secondary);
		}}

		.social-links {{
			display: flex;
			justify-content: center;
			gap: 15px;
			margin: 15px 0;
		}}

		.social-icon {{
			width: 36px;
			height: 36px;
			border-radius: 50%;
			background: var(--bg-accent);
			display: flex;
			align-items: center;
			justify-content: center;
			transition: all 0.3s ease;
		}}

		.social-icon:hover {{
			transform: translateY(-3px);
			background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
		}}

		.social-icon svg {{
			width: 18px;
			height: 18px;
			fill: var(--primary-color);
		}}

		.social-icon:hover svg {{
			fill: white;
		}}

		.footer-links {{
			margin: 15px 0;
		}}

		.footer-links a {{
			color: var(--primary-color);
			text-decoration: none;
			margin: 0 10px;
			transition: all 0.3s ease;
		}}

		.footer-links a:hover {{
			color: var(--secondary-color);
		}}

		.copyright {{
			font-size: 12px;
			color: var(--text-secondary);
			margin-top: 15px;
		}}

		@media (max-width: 650px) {{
			.container {{
				margin: 20px 15px;
			}}

			.header {{
				padding: 20px;
			}}

			.content {{
				padding: 25px 20px;
			}}

			h1 {{
				font-size: 20px;
			}}

			p {{
				font-size: 14px;
			}}

			.code {{
				font-size: 26px;
				padding: 12px 20px;
			}}
		}}
	</style>
</head>

<body class=""dark-mode"">
	<div class=""container"">
		<div class=""header"">
			<div class=""logo"">
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
						<filter id=""glow"" x=""-20%"" y=""-20%"" width=""140%"" height=""140%"">
							<feGaussianBlur stdDeviation=""2"" result=""blur"" />
							<feComposite in=""SourceGraphic"" in2=""blur"" operator=""over"" />
						</filter>
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
					   Z"" fill=""url(#innerGradient)"" filter=""url(#glow)"" />

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

		<div class=""content"">
			{htmlMessage}
		</div>

		<div class=""footer"">
			<p>Connect with us on Sociam!</p>

			<div class=""footer-links"">
				<a href=""mailto:me5260287@gmail.com"">Contact Us</a>
			</div>

			<div class=""copyright"">
				© 2025 Sociam. All rights reserved.
			</div>
		</div>
	</div>

	<script>

		// Countdown timer simulation
		let timeLeft = 180;
		const timerElement = document.querySelector('.timer');

		function updateTimer() {{
			const minutes = Math.floor(timeLeft / 60);
			const seconds = timeLeft % 60;
			timerElement.textContent = `This code will expire in ${{minutes}}:${{seconds < 10 ? '0' : ''}}${{seconds}} minutes`;

			if (timeLeft > 0) {{
				timeLeft--;
				setTimeout(updateTimer, 1000);
			}} else {{
				timerElement.textContent = 'This code has expired';
				timerElement.style.color = '#ff3b30';
			}}
		}}

		updateTimer();
	</script>
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
