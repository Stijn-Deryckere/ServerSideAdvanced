using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IDeviceRepository
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Device> All();
        Webshop.Models.Device GetByID(object id);
        Webshop.Models.Device Insert(Webshop.Models.Device entity);
    }
}
