using System;
namespace Iotshop.BusinessLayer.Repositories
{
    public interface IDeviceRepository
    {
        System.Collections.Generic.IEnumerable<Iotshop.Models.Device> All();
        Iotshop.Models.Device GetByID(object id);
        Iotshop.Models.Device Insert(Iotshop.Models.Device entity);
    }
}
