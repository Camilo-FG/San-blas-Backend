using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SanblasBackend.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            _logger.LogWarning("No se envió correo: destinatario vacío.");
            return false;
        }

        var from = _config["EmailSettings:From"];
        var host = _config["EmailSettings:Host"];
        var portValue = _config["EmailSettings:Port"];
        var username = _config["EmailSettings:Username"];
        var password = _config["EmailSettings:Password"]?.Replace(" ", "");

        if (string.IsNullOrWhiteSpace(from) ||
            string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(portValue) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            _logger.LogError(
                "EmailSettings incompleto. Configure From, Host, Port, Username y Password en Railway.");
            return false;
        }

        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(toEmail.Trim()));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, int.Parse(portValue), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Correo enviado a {Email} con asunto {Subject}", toEmail, subject);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar correo a {Email}", toEmail);
            return false;
        }
    }
}
