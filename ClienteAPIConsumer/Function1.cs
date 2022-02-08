using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace ClienteAPIConsumer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run(
            [KafkaTrigger("BrokerList",
                          "clientes",
                          Username = "%KafkaUserName%",
                          Password = "%KafkaPassword%",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "MS-SAP")] KafkaEventData<string>[] events,
            ILogger log)
        {
            foreach (KafkaEventData<string> eventData in events)
            {
                log.LogInformation($"MS-SAP Processado evento: {eventData.Value}");
            }
        }
    }
}
