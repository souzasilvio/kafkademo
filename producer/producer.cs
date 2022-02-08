using Confluent.Kafka;
using System;
using Microsoft.Extensions.Configuration;

class Producer {
    static void Main(string[] args)
    {
        if (args.Length != 1) {
            args = new string[] { @"C:\Pessoal\kafka-dotnet-getting-started\kafkademo\getting-started.properties" };
           // Console.WriteLine("Please provide the configuration file path as a command line argument");
        }

        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();

        EnviarClientes(configuration, 10);
    }

    private static void EnviarClientes(IConfiguration configuration, int total)
    {
        const string topic = "clientes";

        string[] users = { "eabara", "jsmith", "sgarcia", "jbernard", "htanaka", "awalther" };
        string[] items = { "book", "alarm clock", "t-shirts", "gift card", "batteries" };

        using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
        {
            var numProduced = 0;            
            for (int i = 0; i < total; ++i)
            {
                Random rnd = new Random();
                var user = users[rnd.Next(users.Length)];
                var item = items[rnd.Next(items.Length)];

                producer.Produce(topic, new Message<string, string> { Key = user, Value = item },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
                            numProduced += 1;
                        }
                    });
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }

    private static void EnviarProdutos(IConfiguration configuration, int total)
    {
        const string topic = "produtos";

        using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
        {
            var numProduced = 0;            
            for (int i = 0; i < total; ++i)
            {
                Random rnd = new Random();

                int chave = rnd.Next();

                producer.Produce(topic, new Message<string, string> { Key = chave.ToString(), Value = $"Produto {chave}" },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = {chave} value = Produto { chave}");
                            numProduced += 1;
                        }
                    });
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }
}