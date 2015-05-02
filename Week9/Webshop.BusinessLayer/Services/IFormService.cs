using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IFormService
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.FormTopic> AllFormTopics();
        Webshop.Models.FormTopic FormTopicByID(int id);
    }
}
