using System;
namespace Language.BusinessLayer.Services
{
    public interface IDeviceService
    {
        System.Collections.Generic.IEnumerable<Language.Models.Models.Device> AllDevices();
    }
}
