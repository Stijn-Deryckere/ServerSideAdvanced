using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Week7RaspJobConsole
{
    class Program
    {
        public static MqttClient client;
        public static CloudStorageAccount storageAccount;
        public static CloudTableClient tableClient;
        public static CloudTable table;
        public static int counter;

        static void Main(string[] args)
        {
            counter = 0;

            //Set up Table Storage Connection.
            SetupStorage();

            try
            {
                client = new MqttClient("kristofpi.cloudapp.net");
                String clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
            }

            catch (Exception ex)
            {
                Console.WriteLine("No connection to broker: " + ex.Message);
            }

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        static void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            byte[] data = e.Message;

            String dataString = System.Text.Encoding.Default.GetString(data);
            AddSensorValue(dataString);
        }

        static void SetupStorage()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create table client.
            tableClient = storageAccount.CreateCloudTableClient();

            //Create the table if it doesn't exist.
            table = tableClient.GetTableReference("temperature");
            table.CreateIfNotExists();
        }

        static void AddSensorValue(String data)
        {
            String[] dataParts = data.Split(';');
            
            //Add an entity to the table.
            SensorValue sensorValue = new SensorValue()
            {
                Id = counter,
                Timestamp = new DateTime(Convert.ToInt64(dataParts[2])),
                Temperature = Convert.ToDouble(dataParts[0]),
                Location = dataParts[1]
            };
            sensorValue.PartitionKey = sensorValue.Location;
            sensorValue.RowKey = Convert.ToString(sensorValue.Id);

            TableOperation insertOperation = TableOperation.Insert(sensorValue);
            table.Execute(insertOperation);

            counter++;
        }
    }
}
