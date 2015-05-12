using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Webshop.BusinessLayer.Cache;
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
            return GetDevicesFromCache();
        }

        public Device DeviceById(int id)
        {
            return this.DeviceRepo.GetByID(id);
        }

        public Device AddDevice(Device device)
        {
            return this.DeviceRepo.Insert(device);
        }

        public List<Device> GetDevicesFromCache()
        {
            List<Device> devices = WebshopCache.cache.Get("Devices") as List<Device>;
            if(devices != null)
                return devices;

            RefreshCachedDevices();

            return GetDevicesFromCache();
        }

        public void RefreshCachedDevices()
        {
            IEnumerable<Device> devices = this.DeviceRepo.All().ToList<Device>();
            WebshopCache.cache.Set("Devices", devices);
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

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\", fileName);
            picture.SaveAs(path);

            using(var fileStream = File.OpenRead(path))
            {
                cloudBlockBlob.UploadFromStream(fileStream);
            }

            File.Delete(path);

            return fileName;
        }
    }
}
