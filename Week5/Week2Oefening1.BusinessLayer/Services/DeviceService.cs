﻿using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.Models.DAL;

namespace Week2Oefening1.Models.Services
{
    public class DeviceService : IDeviceService
    {
        private IGenericRepository<OS> repoOS = null;
        private IGenericRepository<Framework> repoFramework = null;
        private IDeviceRepository repoDevice = null;

        public DeviceService(IGenericRepository<OS> repoOS, IGenericRepository<Framework> repoFramework, IDeviceRepository repoDevice)
        {
            this.repoOS = repoOS;
            this.repoFramework = repoFramework;
            this.repoDevice = repoDevice;
        }

        /*
         * Devices
         */

        public IEnumerable<Device> AllDevices()
        {
            return repoDevice.All();
        }

        public Device DeviceById(int id)
        {
            return repoDevice.GetByID(id);
        }

        public Device AddDevice(Device device)
        {
            return repoDevice.Insert(device);
        }

        /*
         * Frameworks
         */

        public IEnumerable<Framework> AllFrameworks()
        {
            return repoFramework.All();
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
            return repoOS.All();
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