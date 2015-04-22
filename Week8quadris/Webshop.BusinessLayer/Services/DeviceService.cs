using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        public Device AddDevice(Device device)
        {
            return this.DeviceRepo.Insert(device);
        }

        /*
         * OSs
         */

        public IEnumerable<OS> AllOSs()
        {
            return this.OSRepo.All();
        }

        public OS OSById(int id)
        {
            return this.OSRepo.GetByID(id);
        }

        /*
         * Frameworks
         */

        public IEnumerable<Framework> AllFrameworks()
        {
            return this.FrameworkRepo.All();
        }

        public Framework FrameworkById(int id)
        {
            return this.FrameworkRepo.GetByID(id);
        }

        /*
         * Image
         */

        public String SaveImage(HttpPostedFileBase image)
        {
            String fileName = Path.GetFileName(image.FileName);
            String path = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + fileName;
            image.SaveAs(path);
            return fileName;
        }
    }
}
