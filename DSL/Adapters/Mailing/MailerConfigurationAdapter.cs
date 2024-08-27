using Microsoft.Extensions.Configuration;

namespace DSL.Adapters.Mailing
{
    public class MailerConfigurationAdapter(IConfiguration configuration)
    {
        public string? SenderName { get; set; } = configuration["Mailer:Administrator:Name"];

        public string? SenderEmail { get; set; } = configuration["Mailer:Administrator:Email"];
        public string? SenderPassword { get; set; } = configuration["Mailer:Administrator:Password"];

        public string? SmtpHost { get; set; } = configuration["Mailer:Administrator:Host"];
        public int SmtpPort { get; set; } = int.Parse(configuration["Mailer:Administrator:Port"]!);
        public bool EnableSSL { get; set; } = bool.Parse(configuration["Mailer:Administrator:SSL"]!);
    }
}