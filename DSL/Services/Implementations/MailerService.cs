using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

using DSL.Adapters.Mailing;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class MailerService(IConfiguration configuration) : IMailerService
    {
        private readonly MailerConfigurationAdapter _cfg = new(configuration);
 
        public async Task MailAsync(string destUser,
            string destEmail, string subject, string htmlMessage)
        {
            using SmtpClient client = new();
            MimeMessage message = new();

            message.From.Add(new MailboxAddress(_cfg.SenderName, _cfg.SenderEmail));
            message.To.Add(new MailboxAddress("", destEmail));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            await client.ConnectAsync(_cfg.SmtpHost, _cfg.SmtpPort, _cfg.EnableSSL);
            await client.AuthenticateAsync(_cfg.SenderEmail, _cfg.SenderPassword);
            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }

        public string PrepareMailHtml(string htmlMessage,
            string selector, params object[] insertFrom)
        {
            for (int idx = 0; idx < insertFrom.Length; idx++)
                htmlMessage = htmlMessage
                    .Replace(selector + idx, insertFrom[idx].ToString());

            return htmlMessage;
        }
    }
}