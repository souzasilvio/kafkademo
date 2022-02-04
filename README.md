# kafkademo
Demonstration project to produce and consume kafka messages. This demo was made using developer account on Confluent Kafka.

This sample implements a producer/consumer pattern as present in this diagraman.
Use:
1. Get this project using VS Code or VS 2022 whith .NET 6 Support.
2. Create a file on solution folder with name "getting-started.properties" and below content:

bootstrap.servers=<Grab Bootstrap server address on your Confluent Kafka portal>
security.protocol=SASL_SSL
sasl.mechanisms=PLAIN
sasl.username=<Grab key name created on API Keys inside your Confluent Kafka portal>
sasl.password=<Grab key secret created on API Keys inside your Confluent Kafka portal>

3. Create a cluster with name clientes. If you prefer other name go ahead and change  topic name on files producer.cs and consumer.cs.
4. Build and run both project. Each project require path name of file  getting-started.properties as you can see on Main metho of producer.cs and consumer.cs. Make sure to adjust this path for your case.
  
![image](https://user-images.githubusercontent.com/31021607/152574380-ee0baa69-351d-4b31-89cc-10fb510340f5.png)
