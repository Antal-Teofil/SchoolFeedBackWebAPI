
namespace Application.Services.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string toName, string subject, string body, string? attachmentPath = null);
    Task SendBulkEmailAsync(IEnumerable<string> toEmails, string subject, string body, string? attachmentPath = null);
}
