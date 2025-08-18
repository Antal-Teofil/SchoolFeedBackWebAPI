using Application.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly string _fromAddress;
    private readonly string _fromName;
    private readonly string _appPassword;
    public EmailService()
    {
        _fromAddress = Environment.GetEnvironmentVariable("EMAIL_FROM_ADDRESS");
        _fromName = Environment.GetEnvironmentVariable("EMAIL_FROM_NAME");
        _appPassword = Environment.GetEnvironmentVariable("EMAIL_APP_PASSWORD");
    }

    public async Task SendEmailAsync(string toEmail, string toName, string subject, string body, string? attachmentPath = null)
    {
        var from = new MailAddress(_fromAddress, _fromName);
        var to = new MailAddress(toEmail, toName);

        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_fromAddress, _appPassword),
            EnableSsl = true
        };

        using var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        if (!string.IsNullOrEmpty(attachmentPath))
        {
            var attachment = new Attachment(attachmentPath);
            message.Attachments.Add(attachment);
        }

        await smtp.SendMailAsync(message);
    }

    public async Task SendBulkEmailAsync(IEnumerable<string> toEmails, string subject, string body, string? attachmentPath = null)
    {
        foreach (var email in toEmails)
        {
            await SendEmailAsync(email, "", subject, body, attachmentPath);
        }
    }

}
