using Microsoft.AspNetCore.Http;

namespace DRY.MailJetClient.Library
{
    public interface IMailjetClientService
    {
        Task<bool> SendAsync(List<string> emails, string message, string subject, List<IFormFile> files);
        Task<bool> SendAsync(string to, string message, string subject, List<IFormFile> files);
        Task<bool> SendAsync(string to, string message, string subject, IFormFile file);
        Task<bool> SendAsync(string to, string message, string subject, MemoryStream stream, string fileName);
        Task<bool> SendAsync(List<string> emails, string message, string subject);
        Task<bool> SendAsync(string to, string message, string subject);
    }
}
