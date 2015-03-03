using System;
namespace Week2Oefening1.Models.Services
{
    public interface IDeviceService
    {
        Week2Oefening1.Models.Device AddDevice(Week2Oefening1.Models.Device device);
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Device> AllDevices();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Framework> AllFrameworks();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.OS> AllOSs();
        Week2Oefening1.Models.Device DeviceById(int id);
        Week2Oefening1.Models.Framework FrameworkById(int id);
        Week2Oefening1.Models.OS OSById(int id);
    }
}
