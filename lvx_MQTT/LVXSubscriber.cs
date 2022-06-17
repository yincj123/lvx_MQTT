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
                //.WithClientId("test123")
<<<<<<< HEAD
                //.WithTcpServer(host,1883)
=======
                .WithTcpServer(host,1883)
                //.WithTcpServer("192.168.178.5",1883)
>>>>>>> bfe230c9a31d06d5d3043abda2e5fc761aecc948
                //.WithTcpServer("192.168.178.5",1883)
                .WithTcpServer("192.168.178.5", 1883)
                //.WithWebSocketServer("mqtt://192.168.178.5:1883")
                //.WithTls(192.168.178.5);
                
                .WithCredentials("apollo","PJn3-mktq")
                .WithCleanSession()
                .Build();
            
            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("connected");
            });

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
            
            client.UseApplicationMessageReceivedHandler(e =>
            {

                Console.WriteLine($"Received Message - {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");

            });
            */

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("disconnected");
            });

            await client.ConnectAsync(option);
            await client.DisconnectAsync();
        }
        
    }
}
