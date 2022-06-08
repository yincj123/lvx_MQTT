using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Threading.Tasks;

namespace lvx_MQTT
{
    class LVXSubscriber
    {
        static async Task Main(string[] args)
        {
            var mqttFactory = new MqttFactory();
            IMqttClient client = mqttFactory.CreateMqttClient();
            var option = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                //.WithClientId("test123")
                .WithTcpServer("mqtt://192.168.178.5",1883)
                //.WithTcpServer("mqtt://192.168.178.5:1883")
                //.WithTcpServer("192.168.178.5",1883)
                .WithCredentials("apollo","PJn3-mktq")
                .WithCleanSession()
                .Build();
            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("connected");
            });
            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("disconnected");
            });
            await client.ConnectAsync(option);
            await client.DisconnectAsync();
        }
        
    }
}
