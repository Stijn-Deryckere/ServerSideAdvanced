using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IDeviceService
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Device> AllDevices();
        System.Collections.Generic.IEnumerable<Webshop.Models.Framework> AllFrameworks();
        System.Collections.Generic.IEnumerable<Webshop.Models.OS> AllOSs();
        Webshop.Models.Device DeviceById(int id);
    }
}
