using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Grazer.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _config;

        public EmailService(ILogger<EmailService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Mailbox", "noreply@grazer")));
            msg.To.Add(MailboxAddress.Parse(email));
            msg.Subject = subject;
            msg.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("localhost");
                await client.SendAsync(msg);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}
