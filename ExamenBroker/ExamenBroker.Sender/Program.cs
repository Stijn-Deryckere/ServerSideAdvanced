using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace ExamenBroker.Sender
{
    public class Program
    {
        public static Timer timer;
        public static MqttClient mqttClient;
        public static int counter;

        static void Main(string[] args)
        {
            counter = 0;

            try
            {
                mqttClient = new MqttClient("cloudpi.cloudapp.net");
                String clientId = Guid.NewGuid().ToString();
                mqttClient.Connect(clientId);
            }

            catch(Exception ex)
            {
                Console.WriteLine("An error occurred while connecting the broker");
            }

            timer = new Timer(10000);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = false;
            timer.Start();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            String text = "Home;Kristof Colpaert;off;" + counter;

            mqttClient.Publish("home/light", Encoding.UTF8.GetBytes(text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            Console.WriteLine(text + ";send");
            counter++;

            timer.Enabled = true;
        }
    }
}
