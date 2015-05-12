using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Calculators;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Services
{
    public class OrderService : Webshop.BusinessLayer.Services.IOrderService
    {
        private IOrderRepository OrderRepo = null;

        public OrderService(IOrderRepository orderRepo)
        {
            this.OrderRepo = orderRepo; 
        }

        public Order MakeOrder(List<BasketItem> basketItems, ApplicationUser user)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach(BasketItem basketItem in basketItems)
            {
                OrderLine orderLine = new OrderLine()
                {
                    Amount = basketItem.Amount,
                    NewDevice = basketItem.NewDevice,
                    RentPrice = basketItem.RentPrice,
                    Timestamp = DateTime.Now
                };
                orderLines.Add(orderLine);
            }

            return new Order()
            {
                NewOrderLines = orderLines,
                NewUser = user,
                Timestamp = DateTime.Now,
                TotalPrice = new TotalPriceCalculator(basketItems).TotalPrice
            };
        }

        public Order AddOrder(Order order)
        {
            String jsonOrder = JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            //References
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("orderqueue");
            queue.CreateIfNotExists();

            //Add message
            CloudQueueMessage message = new CloudQueueMessage(jsonOrder);
            queue.AddMessage(message);

            return order;
        }

        public Order SaveOrder(Order order)
        {
            Order finalOrder = this.OrderRepo.Insert(order);
            SendOrderMail(finalOrder);

            return finalOrder;
        }

        public void SendOrderMail(Order order)
        {
            //Bericht aanmaken.
            var myMessage = new SendGridMessage();

            //Instellingen maken.
            myMessage.From = new MailAddress("kristof@kristofcolpaert.com");
            String recipient = order.NewUser.Firstname + " " + order.NewUser.Name + " <" + order.NewUser.Email + ">";
            List<String> recipients = new List<String>()
            {
                @recipient
            };
            myMessage.AddTo(recipients);
            myMessage.Subject = "Proficiat met uw bestelling!";

            //Bericht vormen.
            String message =
                "<h3>Geachte " + order.NewUser.Firstname + "</h3>" +
                "<p>Wij danken u voor uw eindeloos vertrouwen in onze onderneming. Hieronder kan u een overzicht vinden van de door u bestelde goederen:</p>" +
                "<table border=\"1\"><tr><th>ID</th><th>Product</th><th>Prijs</th></tr>";
            foreach(OrderLine orderLine in order.NewOrderLines)
            {
                message += "<tr><td>" + orderLine.NewDevice.ID + "</td><td>" + orderLine.NewDevice.Name + "</td><td>" + orderLine.RentPrice + "</td></tr>";
            }
            message += "</table><br/><p>Met vriendelijke groeten, uw Kristof Colpaert-shopverantwoordelijke</p>";
            myMessage.Html = message;

            //Bericht verzenden.
            var credentials = new NetworkCredential("kriscolp", "-Password1");
            var transportWeb = new Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
