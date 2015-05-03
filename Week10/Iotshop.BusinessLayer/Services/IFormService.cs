using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface IFormService
    {
        Iotshop.Models.Form AddForm(Iotshop.Models.Form form);
        System.Collections.Generic.IEnumerable<Iotshop.Models.FormTopic> AllFormTopics();
        Iotshop.Models.FormTopic FormTopicByID(int id);
        Iotshop.Models.Form SaveForm(Iotshop.Models.Form form);
    }
}
