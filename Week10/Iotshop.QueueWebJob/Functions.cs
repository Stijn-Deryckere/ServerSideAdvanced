using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Iotshop.Models;
using Iotshop.BusinessLayer.Repositories;
using Iotshop.BusinessLayer.Context;
using Iotshop.BusinessLayer.Services;

namespace Iotshop.QueueWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("orderqueue")] string message, TextWriter log)
        {
            //Object deserialiseren.
            Order order = JsonConvert.DeserializeObject<Order>(message);

            //Repositories en services aanmaken. 
            OrderRepository orderRepo = new OrderRepository(new IotshopContext());
            OrderService orderServ = new OrderService(orderRepo);

            try
            {
                //Object opslaan in database.
                orderServ.SaveOrder(order);
            }
            
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
