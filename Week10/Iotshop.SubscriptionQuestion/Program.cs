using Iotshop.BusinessLayer.Context;
using Iotshop.BusinessLayer.Repositories;
using Iotshop.BusinessLayer.Services;
using Iotshop.Models;
using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Iotshop.SubscriptionQuestion
{
    class Program
    {
        public static Timer timer;

        static void Main(string[] args)
        {
            timer = new Timer(10);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = false;
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }

        public static void timer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            //Context aanmaken.
            IotshopContext context = new IotshopContext();
            IGenericRepository<FormTopic> formTopicRepo = new GenericRepository<FormTopic>(context);
            IFormRepository formRepo = new FormRepository(context);
            IFormService formServ = new FormService(formTopicRepo, formRepo);
            IOrderRepository orderRepo = new OrderRepository(context);
            IOrderService orderServ = new OrderService(orderRepo);

            //Connectie maken en client opvragen.
            String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(connectionString, "websitemessages", "Question");

            //Voorbeeld van de PeekLock receive mode.

            //Callback-opties configureren
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            //Berichten in de subscription opvangen.
            client.OnMessage((message) =>
            {
                try
                {
                    //Bericht verwerken
                    Form tempForm = message.GetBody<Form>();
                    Console.WriteLine(tempForm.Description);

                    Form form = new Form()
                    {
                        NewFormTopic = formServ.FormTopicByID(tempForm.NewFormTopic.ID),
                        Description = tempForm.Description,
                        Email = tempForm.Email,
                        Name = tempForm.Name
                    };

                    if (tempForm.NewOrder != null)
                        form.NewOrder = orderServ.OrderByID(tempForm.NewOrder.ID);

                    //Bericht opslaan in de database
                    formServ.SaveForm(form);

                    //Bericht verwijderen uit subscription
                    message.Complete();
                }

                catch (Exception ex)
                {
                    //Toont aan dat er een probleem is, we unlocken het bericht in de subscription.
                    message.Abandon();
                }
            }, options);
            timer.Enabled = true;
        }
    }
}
