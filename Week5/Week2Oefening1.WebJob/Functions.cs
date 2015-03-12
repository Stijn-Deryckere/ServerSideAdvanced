using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Week2Oefening1.Models;
using Newtonsoft.Json;
using Week2Oefening1.Models.Services;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.BusinessLayer.Context;

namespace Week2Oefening1.WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("iotshop")] string message, TextWriter log)
        {
            OrderRepository orderRepository = new OrderRepository(new ApplicationDbContext());
            OrderService orderService = new OrderService(orderRepository);

            Order order = JsonConvert.DeserializeObject<Order>(message);
            try
            {
                orderService.AddOrderToDatabase(order);
            }

            catch(Exception ex)
            {
                log.WriteLine(ex.Message);
            }
        }
    }
}
