using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Webshop.Models.Models;
using Newtonsoft.Json;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Context;
using Webshop.BusinessLayer.Services;

namespace Webshop.QueueWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("orderqueue")] string message, TextWriter log)
        {
            Order order = JsonConvert.DeserializeObject<Order>(message);

            OrderRepository orderRepo = new OrderRepository(new ApplicationDbContext());

            OrderService orderServ = new OrderService(orderRepo);

            try
            {
                orderServ.SaveOrder(order);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
