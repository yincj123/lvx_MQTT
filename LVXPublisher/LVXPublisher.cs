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
            //Connection Part
            var mqttFactory = new MqttFactory();
            string host = "mqtt://192.168.178.5";
            IMqttClient client = mqttFactory.CreateMqttClient();
            var option = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("192.168.178.5", 1883)
                .WithCredentials("apollo", "PJn3-mktq")
                .WithCleanSession()
                .Build();

            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("connected");
            });
            
            await client.ConnectAsync(option);
            
            

           

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("disconnected");
            });
            
            Console.WriteLine("please press a key to publish the messsage");
            Console.ReadLine();
            await PublishMessageAsync(client);
            await client.DisconnectAsync();
        }

        private static async Task PublishMessageAsync(IMqttClient client)
        {
            string messagePayload = "Hello! I am testing the programm";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("Test")
                .WithPayload(messagePayload)
                .WithAtLeastOnceQoS()
                .Build();
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
                Console.WriteLine($"Published Message - {messagePayload}");
            }
        }

    }
}
