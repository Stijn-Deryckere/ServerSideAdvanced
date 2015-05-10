using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Services
{
    public class DeviceService : Webshop.BusinessLayer.Services.IDeviceService
    {
        IDeviceRepository DeviceRepo = null;
        IGenericRepository<OS> OSRepo = null;
        IGenericRepository<Framework> FrameworkRepo = null;

        public DeviceService(IDeviceRepository deviceRepo, IGenericRepository<OS> oSRepo, IGenericRepository<Framework> frameworkRepo)
        {
            this.DeviceRepo = deviceRepo;
            this.OSRepo = oSRepo;
            this.FrameworkRepo = frameworkRepo;
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
         * Other methods
         */

        public String SaveImage(HttpPostedFileBase picture)
        {
            String fileName = Path.GetFileName(picture.FileName);
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Images\\", fileName);
            picture.SaveAs(path);

            return fileName;
        }
    }
}
