using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models;

namespace Webshop.BusinessLayer.Services
{
    public class DeviceService : Webshop.BusinessLayer.Services.IDeviceService
    {
        private IGenericRepository<OS> OSRepo = null;
        private IGenericRepository<Framework> FrameworkRepo = null;
        private IDeviceRepository DeviceRepo = null;

        public DeviceService(IGenericRepository<OS> OSRepo, IGenericRepository<Framework> FrameworkRepo, IDeviceRepository DeviceRepo)
        {
            this.OSRepo = OSRepo;
            this.FrameworkRepo = FrameworkRepo;
            this.DeviceRepo = DeviceRepo;
        }

        /*
         * Devices
         */

        public IEnumerable<Device> AllDevices()
        {
            return this.DeviceRepo.All();
        }

        public Device DeviceById(int id)
        {
            return this.DeviceRepo.GetByID(id);
        }

        /*
         * OSs
         */

        public IEnumerable<OS> AllOSs()
        {
            return this.OSRepo.All();
        }

        /*
         * Frameworks
         */

        public IEnumerable<Framework> AllFrameworks()
        {
            return this.FrameworkRepo.All();
        }
    }
}
