using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models;

namespace Webshop.BusinessLayer.Services
{
    public class FormService : Webshop.BusinessLayer.Services.IFormService
    {
        private IGenericRepository<FormTopic> FormTopicRepo = null;

        public FormService(IGenericRepository<FormTopic> formTopicRepo)
        {
            this.FormTopicRepo = formTopicRepo;
        }

        /*
         * FormTopics
         */
        public IEnumerable<FormTopic> AllFormTopics()
        {
            return this.FormTopicRepo.All();
        }

        public FormTopic FormTopicByID(int id)
        {
            return this.FormTopicRepo.GetByID(id);
        }

        /*
         * Forms
         */
        public Form AddForm(Form form)
        {
            //Topic-settings instellen
            TopicDescription topicDescription = new TopicDescription("websitemessages");
            topicDescription.MaxSizeInMegabytes = 5120;
            topicDescription.DefaultMessageTimeToLive = new TimeSpan(0, 5, 0);

            //Connectie maken met namespace
            String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            //Indien queue nog niet bestaat, aanmaken
            if(!namespaceManager.TopicExists("websitemessages"))
            {
                namespaceManager.CreateTopic(topicDescription);
            }

            //Subscription aanmaken die alle berichten accepteert
            if(!namespaceManager.SubscriptionExists("websitemessages", "AllMessages"))
            {
                namespaceManager.CreateSubscription("websitemessages", "AllMessages");
            }

            //Subscription aanmaken die berichten met als topic "Problem" accepteert
            SqlFilter problemFiter = new SqlFilter("TopicID = 1");

            if (!namespaceManager.SubscriptionExists("websitemessages", "Problem"))
            {
                namespaceManager.CreateSubscription("websitemessages", "Problem", problemFiter);
            }

            //Subscription aanmaken die berichten met als topic "Question" accepteert
            SqlFilter questionFilter = new SqlFilter("TopicID = 2");

            if(!namespaceManager.SubscriptionExists("websitemessages", "Question"))
            {
                namespaceManager.CreateSubscription("websitemessages", "Question", questionFilter);
            }

            //Maak een TopicClient aan.
            TopicClient topicClient = TopicClient.CreateFromConnectionString(connectionString, "websitemessages");
            
            //Maak een BrokeredMessage aan.
            BrokeredMessage message = new BrokeredMessage(form);
            message.Properties["TopicID"] = form.NewFormTopic.ID;

            //Verzendt het bericht.
            topicClient.Send(message);

            return form;
        }
    }
}
