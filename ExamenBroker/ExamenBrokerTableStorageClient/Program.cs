using ExamenBroker.Receiver;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenBrokerTableStorageClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("iot");

            TableQuery<SensorValue> query = new TableQuery<SensorValue>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Home"));
            foreach(SensorValue sensorValue in table.ExecuteQuery(query))
            {
                Console.WriteLine(sensorValue.Name + sensorValue.RowKey);
            }

            Console.ReadKey();
        }
    }
}
