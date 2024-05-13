using Catalog.Domain;

namespace Catalog.Facade.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    Task SendConfirmationEmail(string email, ApplicationUser user);
    Task<string> ConfirmEmail(string userId, string token);
}