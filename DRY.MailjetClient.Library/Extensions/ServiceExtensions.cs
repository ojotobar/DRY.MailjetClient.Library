using DRY.MailJetClient.Library.Settings;
using Mailjet.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DRY.MailJetClient.Library.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureMailJet(this IServiceCollection services, 
            string apiKey, string apiSecret, string mailjetEmail, string mailjetName)
        {
            services.AddSingleton(_ =>
            {
                return MailSettings.Initialize(mailjetEmail, mailjetName);
            });

            services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            {
                client.UseBasicAuthentication(apiKey, apiSecret);
            });

            services.AddScoped<IMailjetClientService, MailjetClientService>();

            return services;
        }
    }
}
