using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Fundamentos.Azure.Function
{
    public static class FunctionSendMail
    {
        [FunctionName("SendMail")]
        public static void Run([ServiceBusTrigger("pedidos", Connection = "ServiceBusConnString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
