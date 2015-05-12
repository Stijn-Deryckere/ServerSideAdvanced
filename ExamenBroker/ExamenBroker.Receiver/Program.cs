using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ExamenBroker.Receiver
{
    class Program
    {
        public static MqttClient mqttClient;

        static void Main(string[] args)
        {
            try
            {
                mqttClient = new MqttClient("cloudpi.cloudapp.net");
                String clientId = Guid.NewGuid().ToString();
                mqttClient.Connect(clientId);
            }

            catch(Exception ex)
            {
                Console.WriteLine("An error occurred while connecting to the broker");
            }

            mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;
            mqttClient.Subscribe(new String[] { "home/light" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        static void mqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            byte[] data = e.Message;

            String dataString = System.Text.Encoding.Default.GetString(data);
            Console.WriteLine(dataString);
        }
    }
}
