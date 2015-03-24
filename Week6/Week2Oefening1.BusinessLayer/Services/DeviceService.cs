using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.BusinessLayer.Cache;
using Week2Oefening1.Models.DAL;

namespace Week2Oefening1.Models.Services
{
    public class DeviceService : IDeviceService
    {
        private IGenericRepository<OS> repoOS = null;
        private IGenericRepository<Framework> repoFramework = null;
        private IDeviceRepository repoDevice = null;
        private IWebshopCache cacheWebshop = null;

        public DeviceService(IGenericRepository<OS> repoOS, IGenericRepository<Framework> repoFramework, IDeviceRepository repoDevice, IWebshopCache cacheWebshop)
        {
            this.repoOS = repoOS;
            this.repoFramework = repoFramework;
            this.repoDevice = repoDevice;
            this.cacheWebshop = cacheWebshop;
        }

        /*
         * Devices
         */

        public IEnumerable<Device> AllDevices()
        {
            return cacheWebshop.GetDevicesFromCache();
        }

        public Device DeviceById(int id)
        {
            return repoDevice.GetByID(id);
        }

        public Device AddDevice(Device device)
        {
            device = repoDevice.Insert(device);
            cacheWebshop.RefreshCacheDevices();
            return device;
        }

        /*
         * Frameworks
         */

        public IEnumerable<Framework> AllFrameworks()
        {
            return cacheWebshop.GetFrameworksFromCache();
        }

        public Framework FrameworkById(int id)
        {
            return repoFramework.GetByID(id);
        }

        /*
         * OS
         */

        public IEnumerable<OS> AllOSs()
        {
            return cacheWebshop.GetOSsFromCache();
        }

        public OS OSById(int id)
        {
            return repoOS.GetByID(id);
        }

        /*
         * Images
         */
        public void AddImage(HttpPostedFileBase imageFile)
        {
            if (imageFile.ContentLength > 0)
            {
                //Retrieve storage account from connection string and create blob client.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                //Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                
                //Create or overwrite the "myblob" blob with contents from a local file.
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageFile.FileName);

                //Create or overwrite the "myblob" blob with contents from a local file. 
                blockBlob.UploadFromStream(imageFile.InputStream);
            }
        }
    }
}