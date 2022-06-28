using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace lvx_MQTT
{
    class LVXSubscriber
    {
       
        static async Task Main(string[] args)
        {
            var mqttFactory = new MqttFactory();
            string host = "mqtt://192.168.178.5";
            IMqttClient client = mqttFactory.CreateMqttClient();
            var option = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString()) 
                .WithTcpServer("192.168.178.5", 1883)               
                .WithCredentials("apollo","PJn3-mktq")
                .WithCleanSession()
                .Build();

            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("connected");
            });
            client.ConnectAsync(option).GetAwaiter().GetResult();
            /*
            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("connected");
                var topicFilter = new TopicFilterBuilder()
                                      .WithTopic("")
                                      .Build();
                await client.SubscribeAsync(topicFilter);
            }
               );
               */

            // Subscribe
            client.UseApplicationMessageReceivedHandler(e =>
            {
                
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("subscribe successfully");
                // Console.WriteLine($"Received Message - {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine();


            });
            //var topic = new MqttTopicFilterBuilder().WithTopic("glp/0/./=system/alarm/copyright").Build();
            //client.SubscribeAsync(topic).GetAwaiter().GetResult();

            await client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("glp/0/./=system/connection/name").Build());
            await client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("Test").Build());

            //Console.WriteLine("subscribe successfully");

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("disconnected");
            });
            Console.ReadLine();
            
            await client.DisconnectAsync();
        }
        
    }
}
