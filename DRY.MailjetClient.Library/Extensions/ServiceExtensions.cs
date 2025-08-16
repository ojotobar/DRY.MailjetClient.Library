using DRY.MailJetClient.Library.Settings;
using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DRY.MailJetClient.Library.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureMailJet(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("DryMailClient")
                .Get<MailSettings>() ?? throw new ArgumentNullException();

            services.Configure<MailSettings>(configuration.GetSection("DryMailClient"));
            services.AddHttpClient<IMailjetClient, MailjetClient>(sp =>
            {
                sp.UseBasicAuthentication(settings.ApiKey, settings.ApiSecret);
            });

            services.AddScoped<IMailjetClientService, MailjetClientService>();

            return services;
        }
    }
}
