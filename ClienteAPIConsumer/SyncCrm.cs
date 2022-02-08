using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClienteAPIConsumer
{
    public class SyncCrm
    {
        // KafkaTrigger sample 
        // Consume the message from "clientes" on the LocalBroker.
        // Add `BrokerList` and `KafkaPassword` to the local.settings.json
        // For EventHubs
        // "BrokerList": "{EVENT_HUBS_NAMESPACE}.servicebus.windows.net:9093"
        // "KafkaPassword":"{EVENT_HUBS_CONNECTION_STRING}
        [FunctionName("SyncCrm")]
        public void Run([KafkaTrigger("BrokerList",
                          "clientes",
                          Username = "%KafkaUserName%",
                          Password = "%KafkaPassword%",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "MS-CRM")] KafkaEventData<string>[] events,
            ILogger log)
        {
            var rep = new Repositorio.RepositorioCliente();
            
            foreach (KafkaEventData<string> eventData in events)
            {
                log.LogInformation($"MS-CRM Processado evento: {eventData.Value}");
                var cliente = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Cliente>(eventData.Value);
                rep.Inserir(cliente);

            }
        }
    }
}
