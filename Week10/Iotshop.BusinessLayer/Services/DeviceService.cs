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
using Iotshop.BusinessLayer.Repositories;
using Iotshop.Models;
using Microsoft.Azure;

namespace Iotshop.BusinessLayer.Services
{
    public class DeviceService : Iotshop.BusinessLayer.Services.IDeviceService
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

            //We verschaffen onszelf toegang to de Storage Account via de ConnectionString.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Aanmaken van de Blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Referentie naar de container images achterhalen.
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("images");

            //Referentie naar de image ophalen.
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

            //We slaan het bestand eerst even lokaal op.
            String path = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + fileName;
            image.SaveAs(path);

            //De blob met als naam de filenaam van de image aanmaken of overschrijven met een lokaal bestand.
            using(var fileStream = System.IO.File.OpenRead(path))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            //We deleten het bestand opnieuw lokaal.
            File.Delete(path);

            return fileName;
        }
    }
}
