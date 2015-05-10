using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IDeviceRepository
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.Device> All();
        Webshop.Models.Models.Device GetByID(object id);
        Webshop.Models.Models.Device Insert(Webshop.Models.Models.Device entity);
    }
}
