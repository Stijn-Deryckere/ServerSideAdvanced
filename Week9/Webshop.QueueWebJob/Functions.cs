using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Webshop.Models;
using Newtonsoft.Json;
using Webshop.BusinessLayer.Services;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Context;

namespace Webshop.QueueWebJob
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
            OrderRepository orderRepo = new OrderRepository(new WebshopContext());
            OrderService orderServ = new OrderService(orderRepo);

            //Object opslaan in database.
            orderServ.SaveOrder(order);
        }
    }
}
