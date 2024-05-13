using Catalog.Domain;
using Catalog.Facade.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Catalog.Service;

public class EmailService(IConfiguration configuration, UserManager<ApplicationUser> userManager) : IEmailService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
    {
        string mailServer = _configuration["EmailSettings:MailServer"]!;
        string fromEmail = _configuration["EmailSettings:FromEmail"]!;
        string password = _configuration["EmailSettings:Password"]!;
        int port = int.Parse(_configuration["EmailSettings:MailPort"]!);

        var client = new SmtpClient(mailServer, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };
        MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body)
        {
            IsBodyHtml = isBodyHTML
        };

        return client.SendMailAsync(mailMessage);
    }

    public async Task SendConfirmationEmail(string email, ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"https://localhost:7215/confirm-email?UserId={user.Id}&Token={token}";
        await SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>;.", true);
    }

    public async Task<string> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (userId is null || token is null)
        {
            return "Link expired";
        }
        else if (user is null)
        {
            return "User not found";
        }
        else
        {
            token = token.Replace(" ", "+");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Thank you for confirming email";
            }
            else
            {
                return "Email not confirmed";
            }
        }
    }
}