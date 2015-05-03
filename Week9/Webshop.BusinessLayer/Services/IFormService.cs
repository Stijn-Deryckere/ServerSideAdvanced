using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IFormService
    {
        Webshop.Models.Form AddForm(Webshop.Models.Form form);
        System.Collections.Generic.IEnumerable<Webshop.Models.FormTopic> AllFormTopics();
        Webshop.Models.FormTopic FormTopicByID(int id);
        Webshop.Models.Form SaveForm(Webshop.Models.Form form);
    }
}
