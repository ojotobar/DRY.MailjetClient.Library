using CSharpTypes.Extensions.String;

namespace DRY.MailJetClient.Library.Settings
{
    public class MailSettings
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string CustomId { get; set; } = string.Empty;

        public static MailSettings Initialize(string email, string name, string customId = "")
        {
            return new MailSettings
            {
                SenderEmail = email,
                SenderName = name,
                CustomId = customId.IsNotNullOrEmpty() ? customId : name,
            };
        }
    }
}
