using CSharpTypes.Extensions.String;

namespace DRY.MailJetClient.Library.Settings
{
    public class MailSettings
    {
        /// <summary>
        /// Mailjet Api Key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// Mailjet Api Secret
        /// </summary>
        public string ApiSecret { get; set; } = string.Empty;
        /// <summary>
        /// The Email Registered with Mailjet
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The Name you want display as the sender
        /// </summary>
        public string AppName { get; set; } = string.Empty;
        /// <summary>
        /// OPtion Custom Id
        /// </summary>
        public string CustomId { get; set; } = string.Empty;
    }
}
