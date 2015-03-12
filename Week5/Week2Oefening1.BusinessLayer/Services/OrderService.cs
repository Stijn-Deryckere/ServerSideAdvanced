using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.Models.DAL;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure;

namespace Week2Oefening1.Models.Services
{
    public class OrderService : Week2Oefening1.BusinessLayer.Services.IOrderService 
    {
        private IOrderRepository repoOrder = null;

        public OrderService(IOrderRepository repoOrder)
        {
            this.repoOrder = repoOrder;
        }

        /*
         * Orders
         */
        public IEnumerable<Order> AllOrders()
        {
            return repoOrder.All();
        }

        public IEnumerable<Order> AllOrdersOfUser(String id)
        {
            return repoOrder.AllOfUser(id);
        }

        public Order AddOrder(Order order)
        {
            String json = JsonConvert.SerializeObject(order);

            /*
             * Set up queue 
             */

            //Retrieve storage account from connectionString.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            //Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("iotshop");

            //Create the queue if it doesn't already exist. 
            queue.CreateIfNotExists();

            /*
             * Insert into queue
             */

            //Create a message and add it to the queue. 
            CloudQueueMessage message = new CloudQueueMessage(json);
            queue.AddMessage(message);

            //return repoOrder.Insert(order);
            return order;
        }

        public Order AddOrderToDatabase(Order order)
        {
            return repoOrder.Insert(order);
        }

        public Order MakeOrder(IEnumerable<BasketItem> basketItems)
        {
            Order order = new Order()
            {
                IsPaid = false,
                Timestamp = DateTime.Now,
                User = basketItems.First<BasketItem>().RentUser,
                TotalPrice = 0,
                OrderLines = new List<OrderLine>()
            };

            ItemCountPrice icp = new ItemCountPrice();
            foreach(BasketItem basketItem in basketItems)
            {
                OrderLine orderLine = new OrderLine()
                {
                    Amount = basketItem.Amount,
                    RentDevice = basketItem.RentDevice,
                    RentingPrice = basketItem.RentDevice.RentingPrice
                };

                order.OrderLines.Add(orderLine);

                icp.DeviceAmounts.Add(orderLine);
            }

            order.TotalPrice = icp.TotalPrice;

            return order;
        }

        public Order MakeOrder(IEnumerable<BasketItem> basketItems, Order previousOrder)
        {
            Order order = MakeOrder(basketItems);
            order.Address = previousOrder.Address;
            order.City = previousOrder.City;
            order.Zipcode = previousOrder.Zipcode;
            order.Name = previousOrder.Name;
            order.FirstName = previousOrder.FirstName;

            return order;
        }
    }
}