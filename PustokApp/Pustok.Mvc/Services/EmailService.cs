using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Pustok.Mvc.Dto;

namespace Pustok.Mvc.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);
}

public class EmailService(
    IOptions<EmailSettings> emailsettings
    ) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailsettings.Value.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        if(isHtml)
            email.Body = new TextPart(TextFormat.Html) { Text = body };
        else
            email.Body = new TextPart(TextFormat.Text) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailsettings.Value.SmtpHost, emailsettings.Value.SmtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailsettings.Value.Username, emailsettings.Value.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
