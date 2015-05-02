using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models;

namespace Webshop.BusinessLayer.Services
{
    public class OrderService : Webshop.BusinessLayer.Services.IOrderService
    {
        private IOrderRepository OrderRepo = null;

        public OrderService(IOrderRepository orderRepo)
        {
            this.OrderRepo = orderRepo;
        }

        /*
         * Put an order in the queue.
         */
        public Order AddOrder(Order order)
        {
            //Serialize order to JSON-string.
            String jsonOrder = JsonConvert.SerializeObject(order);

            //Get ConnectionString.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the queue client. 
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            //Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("orderqueue");

            //Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            //Create a message and add it to the queue. 
            CloudQueueMessage message = new CloudQueueMessage(jsonOrder);
            queue.AddMessage(message);

            return order;
        }

        /*
         * Save order to database.
         */
        public Order SaveOrder(Order order)
        {
            return this.OrderRepo.Insert(order);
        }

        public IEnumerable<Order> OrdersByApplicationUser(ApplicationUser user)
        {
            return this.OrderRepo.GetByApplicationUser(user);
        }

        public Order GenerateOrder(ApplicationUser user, List<BasketItem> basketItems, double totalPrice)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach(BasketItem basketItem in basketItems)
            {
                OrderLine orderLine = new OrderLine()
                {
                    Amount = basketItem.Amount,
                    RentingPrice = basketItem.RentingPrice,
                    NewDevice = basketItem.NewDevice,
                    Timestamp = DateTime.Now
                };
                orderLines.Add(orderLine);
            }

            Order order = new Order()
            {
                NewOrderLines = orderLines,
                NewUser = user,
                Timestamp = DateTime.Now,
                TotalPrice = totalPrice
            };

            return order;
        }
    }
}
