using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Week7RaspConsole
{
    class Program
    {
        public static MqttClient client;
        public static Timer timer;

        //Initialize the timer.
        static void Main(string[] args)
        {
            try
            {
                client = new MqttClient("kristofpi.cloudapp.net");
                String clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
            }

            catch(Exception ex)
            {
                Console.WriteLine("No connection to broker: " + ex.Message);
            }

            timer = new Timer(600000);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = false;
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }

        //Code to execute every 10 seconds.
        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Write("Fetching temperature... ");
            double temperature = GetTemperature(0);
            Console.Write(temperature.ToString() + "°C");
            String stringTemperature = temperature + ";" + "A113;" + DateTime.Now.Ticks;

            client.Publish("/home/temperature", Encoding.UTF8.GetBytes(stringTemperature), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            Console.WriteLine(" ...done");
            timer.Enabled = true;
        }

        //Get temperature sensor value.
        static double GetTemperature(int sensorIndex)
        {
            //Find directory with all devices and select the temperature sensor.
            DirectoryInfo devicesDir = new DirectoryInfo("/sys/bus/w1/devices");
            DirectoryInfo deviceDir = devicesDir.GetDirectories("28*")[sensorIndex];

            //Read the input of the temperature sensor.
            using (StreamReader reader = new StreamReader(deviceDir.FullName + "/w1_slave"))
            {
                String w1SlaveText = reader.ReadToEnd();
                String temporaryTemperature = w1SlaveText.Split(new String[] { "t=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                double temperature = double.Parse(temporaryTemperature) / 1000;
                return temperature;
            }
        }
    }
}
