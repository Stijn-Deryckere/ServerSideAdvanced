using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface IDeviceService
    {
        Iotshop.Models.Device AddDevice(Iotshop.Models.Device device);
        System.Collections.Generic.IEnumerable<Iotshop.Models.Device> AllDevices();
        System.Collections.Generic.IEnumerable<Iotshop.Models.Framework> AllFrameworks();
        System.Collections.Generic.IEnumerable<Iotshop.Models.OS> AllOSs();
        Iotshop.Models.Device DeviceById(int id);
        Iotshop.Models.Framework FrameworkById(int id);
        Iotshop.Models.OS OSById(int id);
        string SaveImage(System.Web.HttpPostedFileBase image);
    }
}
