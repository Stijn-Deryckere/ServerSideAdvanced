using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
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
        public static CloudStorageAccount storageAccount;
        public static CloudTableClient tableClient;
        public static CloudTable table;
        public static int counter;

        static void Main(string[] args)
        {
            SetupStorage();

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
            //AddValue(dataString);
        }

        public static void SetupStorage()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference("iot");
            table.CreateIfNotExists();
        }

        public static void AddValue(String data)
        {
            String[] dataParts = data.Split(';');
            SensorValue sensorValue = new SensorValue()
            {
                PartitionKey = dataParts[0],
                RowKey = dataParts[3],
                Name = dataParts[1]
            };

            TableOperation insertOperation = TableOperation.Insert(sensorValue);
            table.Execute(insertOperation);
        }
    }
}
