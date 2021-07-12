using System.Net;
using System.Threading.Tasks;
using BlazorTemplater;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using MimeKit;
using NRCan.Datahub.Shared.Templates;



namespace NRCan.Datahub.Shared.Services
{
    // public class EmailConfiguration
    // {
    //     public string SmtpHost { get; set; }
    //     public int SmtpPort { get; set; }
    //     public string SmtpUsername { get; set; }
    //     public string SmtpPassword { get; set; }
    //     public string SenderName { get; set; }
    //     public string SenderAddress { get; set; }
    // }

    public class EmailNotificationService: IEmailNotificationService
    {
        private static readonly string SMTP_HOST_KEY = "EmailNotification:SmtpHost";
        private static readonly string SMTP_PORT_KEY = "EmailNotification:SmtpPort";
        private static readonly string SMTP_USERNAME_KEY = "EmailNotification:SmtpUsername";
        private static readonly string SMTP_PASSWORD_KEY = "EmailNotification:SmtpPassword";
        private static readonly string SENDER_NAME_KEY = "EmailNotification:SenderName";
        private static readonly string SENDER_ADDRESS_KEY = "EmailNotification:SenderAddress";

        private IConfiguration _config;

        // private IStringLocalizer<SharedResources> _localizer;

        public EmailNotificationService(
            //IStringLocalizer<SharedResources> localizer,
            IConfiguration config
            )
        {
            // _localizer = localizer;
            _config = config;
        }

        public string RenderTemplate()
        {
            string html = new ComponentRenderer<TestEmailTemplate>()
                // .AddService<IStringLocalizer>(_localizer)
                .Render();
            return html;
        }

        public async Task SendEmailMessage(string subject, string body, string recipientName, string recipientAddress, bool isHtml = true)
        {
            var senderName = _config.GetValue<string>(SENDER_NAME_KEY);
            var senderAddress = _config.GetValue<string>(SENDER_ADDRESS_KEY);

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(senderName, senderAddress));
            msg.To.Add(new MailboxAddress(recipientName, recipientAddress));
            msg.Subject = subject;
            var bodyPart = new TextPart(isHtml? "html": "plain") 
            {
                Text = body
            };

            var smtpHost = _config.GetValue<string>(SMTP_HOST_KEY);
            var smtpPort = _config.GetValue<int>(SMTP_PORT_KEY);
            var smtpUsername = _config.GetValue<string>(SMTP_USERNAME_KEY);
            var smtpPassword = _config.GetValue<string>(SMTP_PASSWORD_KEY);

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(new NetworkCredential(smtpUsername, smtpPassword));

                await smtpClient.SendAsync(msg);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}