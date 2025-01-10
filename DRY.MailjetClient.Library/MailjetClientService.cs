using Mailjet.Client.Resources;
using Mailjet.Client;
using Newtonsoft.Json.Linq;
using DRY.MailJetClient.Library.Settings;
using Microsoft.AspNetCore.Http;

namespace DRY.MailJetClient.Library
{
    public class MailjetClientService : IMailjetClientService
    {
        private readonly IMailjetClient mailClient;
        private readonly MailSettings settings;

        public MailjetClientService(IMailjetClient mailClient, MailSettings settings)
        {
            this.mailClient = mailClient;
            this.settings = settings;
        }

        /// <summary>
        /// Send single mail without attachment
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(string to, string message, string subject)
        {
            MailjetResponse response = null!;
            var emails = new List<string>() { to };

            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email",settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "CustomId", settings.CustomId }
                        }
                    });
                    response = await mailClient.PostAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response?.IsSuccessStatusCode ?? false;
        }

        /// <summary>
        /// Send email to multiple users
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(List<string> emails, string message, string subject)
        {
            var responses = new List<MailjetResponse>();

            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email",settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "CustomId", settings.CustomId }
                        }
                    });
                    var response = await mailClient.PostAsync(request);
                    responses.Add(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responses.All(r => r?.IsSuccessStatusCode ?? false);
        }

        /// <summary>
        /// Send single mail with attachment
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(string to, string message, string subject, 
            MemoryStream stream, string fileName)
        {
            var fileBytes = stream.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);

            MailjetResponse response = null!;
            var emails = new List<string>() { to };

            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email", settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "Attachments", new JArray
                                {
                                    new JObject
                                    {
                                        {"ContentType", "text/plain" },
                                        {"Filename", fileName },
                                        {"Base64Content", base64String }
                                    }
                                 }
                            },
                            { "CustomId", settings.CustomId }
                        }
                    });
                    response = await mailClient.PostAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response?.IsSuccessStatusCode ?? false;
        }

        /// <summary>
        /// Send single mail with attachment
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(string to, string message, string subject, IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            var fileBytes = stream.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);

            MailjetResponse response = null!;
            var emails = new List<string>() { to };

            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email", settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "Attachments", new JArray
                                {
                                    new JObject
                                    {
                                        {"ContentType", "text/plain" },
                                        {"Filename", file.FileName },
                                        {"Base64Content", base64String }
                                    }
                                 }
                            },
                            { "CustomId", "SwappaApp" }
                        }
                    });
                    response = await mailClient.PostAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response?.IsSuccessStatusCode ?? false;
        }

        /// <summary>
        /// Send single mail with multiple attachments
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(string to, string message, string subject, List<IFormFile> files)
        {
            MailjetResponse response = null!;
            var emails = new List<string>() { to };

            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email", settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "Attachments", GetAttachments(files)},
                            { "CustomId", settings.CustomId }
                        }
                    });
                    response = await mailClient.PostAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response?.IsSuccessStatusCode ?? false;
        }

        /// <summary>
        /// Sends email to multiple users with multiple attachments
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(List<string> emails, string message, string subject, List<IFormFile> files)
        {
            var responses = new List<MailjetResponse>();
            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                     .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email", settings.SenderEmail},
                                    {"Name", settings.SenderName}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", subject },
                            { "TextPart", message },
                            { "HtmlPart", message },
                            { "Attachments", GetAttachments(files)},
                            { "CustomId", settings.CustomId }
                        }
                    });
                    var response = await mailClient.PostAsync(request);
                    responses.Add(response);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responses.All(r => r?.IsSuccessStatusCode ?? false);
        }

        private JArray GetAttachments(List<IFormFile> files)
        {
            var response = new JArray();
            foreach (var file in files)
            {
                using var stream = new MemoryStream();
                file.CopyTo(stream);
                var fileBytes = stream.ToArray();
                var base64String = Convert.ToBase64String(fileBytes);

                response.Add(new JObject
                {
                    new JObject
                    {
                        {"ContentType", "text/plain" },
                        {"Filename", file.FileName },
                        {"Base64Content", base64String }
                    }
                });
            }

            return response;
        }
    }
}