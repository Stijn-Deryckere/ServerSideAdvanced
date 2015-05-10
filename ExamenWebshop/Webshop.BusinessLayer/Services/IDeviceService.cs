using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IDeviceService
    {
        Webshop.Models.Models.Device AddDevice(Webshop.Models.Models.Device device);
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.Device> AllDevices();
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.Framework> AllFrameworks();
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.OS> AllOSs();
        Webshop.Models.Models.Device DeviceById(int id);
        Webshop.Models.Models.Framework FrameworkById(int id);
        Webshop.Models.Models.OS OSById(int id);
        string SaveImage(System.Web.HttpPostedFileBase picture);
    }
}
