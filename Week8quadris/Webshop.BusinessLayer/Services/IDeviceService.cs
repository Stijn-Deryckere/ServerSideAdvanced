using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IDeviceService
    {
        Webshop.Models.Device AddDevice(Webshop.Models.Device device);
        System.Collections.Generic.IEnumerable<Webshop.Models.Device> AllDevices();
        System.Collections.Generic.IEnumerable<Webshop.Models.Framework> AllFrameworks();
        System.Collections.Generic.IEnumerable<Webshop.Models.OS> AllOSs();
        Webshop.Models.Device DeviceById(int id);
        Webshop.Models.Framework FrameworkById(int id);
        Webshop.Models.OS OSById(int id);
        string SaveImage(System.Web.HttpPostedFileBase image);
    }
}
