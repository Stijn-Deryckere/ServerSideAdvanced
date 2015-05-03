using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Services;
using Webshop.Models;

namespace Webshop.SubscriptionQuestion
{
    class Program
    {
        static void Main(string[] args)
        {
            //Context aanmaken.
            WebshopContext context = new WebshopContext();
            IGenericRepository<FormTopic> formTopicRepo = new GenericRepository<FormTopic>(context);
            IFormRepository formRepo = new FormRepository(context);
            IFormService formServ = new FormService(formTopicRepo, formRepo);

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
                    Form form = message.GetBody<Form>();

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
        }
    }
}
