using System.Threading.Tasks;

namespace NRCan.Datahub.Shared.Services
{
    public interface IEmailNotificationService
    {
        string RenderTemplate();
        Task SendEmailMessage(string subject, string body, string recipientName, string recipientAddress, bool isHtml = true);
    }
}