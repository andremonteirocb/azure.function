using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Infra.CrossCutting.Settings;
using Infra.CrossCutting;
using System.Net.Mail;
using System.Net;

namespace Azure.Function.HttpTrigger.SendMail
{
    public class FunctionHttpTrigger
    {
        private EmailSettings _emailSettings;
        public FunctionHttpTrigger(EmailSettings options)
        {
            _emailSettings = options;
        }

        [FunctionName("SendMail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger logger)
        {
            logger.LogInformation($"Iniciando o processo de envio!");

            var json = await new StreamReader(req.Body).ReadToEndAsync();
            var email = JsonConvert.DeserializeObject<Email>(json);

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

            logger.LogInformation($"E-mail envio com sucesso!");

            return new OkObjectResult("");
        }
    }
}
