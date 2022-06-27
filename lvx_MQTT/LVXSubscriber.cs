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
        [Obsolete]
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
            await client.ConnectAsync(option);

            
                
            await client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("").Build());

            Console.WriteLine("subscribe successfully");

            client.UseApplicationMessageReceivedHandler(e =>
            {

                // Console.WriteLine($"Received Message - {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();


            });


            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("disconnected");
            });

            
            await client.DisconnectAsync();
        }
        
    }
}
