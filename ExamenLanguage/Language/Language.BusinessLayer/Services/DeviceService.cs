using Language.BusinessLayer.Repositories;
using Language.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.BusinessLayer.Services
{
    public class DeviceService : Language.BusinessLayer.Services.IDeviceService
    {
        private IGenericRepository<Device> DeviceRepo = null;

        public DeviceService(IGenericRepository<Device> deviceRepo)
        {
            this.DeviceRepo = deviceRepo;
        }

        public IEnumerable<Device> AllDevices()
        {
            return this.DeviceRepo.All();
        }
    }
}
