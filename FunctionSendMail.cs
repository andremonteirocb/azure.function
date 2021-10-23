using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentos.Azure.Function
{
    public class FunctionSendMail
    {
        private EmailSettings _emailSettings;
        public FunctionSendMail(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        [FunctionName("SendMail")]
        public async Task Run([ServiceBusTrigger("emails", Connection = "ServiceBusConnString")] Message message,
            ILogger logger,
            MessageReceiver messageReceiver)
        {
            try
            {
                logger.LogInformation($"Iniciando o processo de envio!");

                var json = Encoding.UTF8.GetString(message.Body);
                var email = JsonConvert.DeserializeObject<OutgoingEmail>(json);

                using (MailMessage mail = new MailMessage(_emailSettings.From, email.To, email.Subject, email.Body))
                {
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(_emailSettings.Smtp, _emailSettings.Port))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password);
                        smtp.Send(mail);
                    }
                }

                await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);

                logger.LogInformation($"Processo de envio finalizado!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                await messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);
            }
        }
    }
}
