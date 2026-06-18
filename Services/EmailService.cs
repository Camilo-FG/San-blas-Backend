using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SanblasBackend.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public EmailService(
        IConfiguration config,
        ILogger<EmailService> logger,
        IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            _logger.LogWarning("No se envió correo: destinatario vacío.");
            return false;
        }

        var resendApiKey = _config["Resend:ApiKey"]?.Trim();
        if (!string.IsNullOrWhiteSpace(resendApiKey))
        {
            return await EnviarConResendAsync(resendApiKey, toEmail, subject, body);
        }

        return await EnviarConSmtpAsync(toEmail, subject, body);
    }

    private async Task<bool> EnviarConResendAsync(
        string apiKey,
        string toEmail,
        string subject,
        string body)
    {
        var from = _config["Resend:From"]?.Trim()
            ?? _config["EmailSettings:From"]?.Trim()
            ?? "onboarding@resend.dev";

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new ResendEmailRequest
            {
                From = from,
                To = [toEmail.Trim()],
                Subject = subject,
                Html = body,
            };

            using var response = await client.PostAsJsonAsync(
                "https://api.resend.com/emails",
                payload);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Correo enviado vía Resend a {Email}", toEmail);
                return true;
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "Resend rechazó el correo a {Email}. Status {Status}. Respuesta: {Body}",
                toEmail,
                (int)response.StatusCode,
                errorBody);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al enviar correo vía Resend a {Email}: {Error}",
                toEmail,
                ObtenerMensajeError(ex));
            return false;
        }
    }

    private async Task<bool> EnviarConSmtpAsync(string toEmail, string subject, string body)
    {
        var from = _config["EmailSettings:From"]?.Trim();
        var host = _config["EmailSettings:Host"]?.Trim();
        var portValue = _config["EmailSettings:Port"]?.Trim();
        var username = _config["EmailSettings:Username"]?.Trim();
        var password = _config["EmailSettings:Password"]?.Replace(" ", "").Trim();

        if (string.IsNullOrWhiteSpace(from) ||
            string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(portValue) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            _logger.LogError(
                "EmailSettings incompleto. En Railway use Resend__ApiKey o configure SMTP.");
            return false;
        }

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from));
        email.To.Add(MailboxAddress.Parse(toEmail.Trim()));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        var intentos = new List<(int Port, SecureSocketOptions SocketOptions)>
        {
            (int.Parse(portValue), ObtenerSocketOptions(int.Parse(portValue))),
            (587, SecureSocketOptions.StartTls),
            (465, SecureSocketOptions.SslOnConnect),
        }.Distinct().ToList();

        Exception? ultimoError = null;

        foreach (var (port, socketOptions) in intentos)
        {
            try
            {
                using var smtp = new SmtpClient();
                smtp.Timeout = 15000;
                await smtp.ConnectAsync(host, port, socketOptions);
                await smtp.AuthenticateAsync(username, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation(
                    "Correo enviado por SMTP a {Email} ({Host}:{Port})",
                    toEmail,
                    host,
                    port);
                return true;
            }
            catch (Exception ex)
            {
                ultimoError = ex;
                _logger.LogWarning(
                    "Fallo SMTP en {Host}:{Port} hacia {Email}: {Error}",
                    host,
                    port,
                    toEmail,
                    ObtenerMensajeError(ex));
            }
        }

        _logger.LogError(
            ultimoError,
            "SMTP no disponible hacia {Email}. En Railway configure Resend__ApiKey.",
            toEmail);

        return false;
    }

    private static SecureSocketOptions ObtenerSocketOptions(int port) =>
        port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

    private static string ObtenerMensajeError(Exception ex) =>
        ex.InnerException is null ? ex.Message : $"{ex.Message} | {ex.InnerException.Message}";

    private sealed class ResendEmailRequest
    {
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        [JsonPropertyName("to")]
        public List<string> To { get; set; } = [];

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("html")]
        public string Html { get; set; } = string.Empty;
    }
}
