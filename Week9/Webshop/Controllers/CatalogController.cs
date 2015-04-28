using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Services;
using Webshop.Models;
using Webshop.Models.PresentationModels;

namespace Webshop.Controllers
{
    public class CatalogController : Controller
    {
        private IDeviceService DeviceService = null;

        public CatalogController(IDeviceService ServiceDevice)
        {
            this.DeviceService = ServiceDevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = this.DeviceService.AllDevices().ToList<Device>();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            DeviceBasketItemPM deviceBasketItemPM = new DeviceBasketItemPM()
            {
                NewDevice = this.DeviceService.DeviceById(id)
            };

            BasketItem basketItem = new BasketItem()
            {
                NewDevice = deviceBasketItemPM.NewDevice
            };

            deviceBasketItemPM.NewBasketItem = basketItem;
            return View(deviceBasketItemPM);
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public ActionResult Add()
        {
            DevicePM devicePM = new DevicePM();

            Device device = new Device();
            devicePM.NewDevice = device;

            devicePM.Frameworks = new SelectList(this.DeviceService.AllFrameworks(), "ID", "Name");
            devicePM.OSs = new SelectList(this.DeviceService.AllOSs(), "ID", "Name");

            return View(devicePM);
        }

        [Authorize(Roles="Administrator")]
        [HttpPost]
        public ActionResult Add(DevicePM devicePM)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Add");

            List<OS> oss = new List<OS>();
            foreach(int i in devicePM.NewOperatingSystems)
                oss.Add(this.DeviceService.OSById(i));

            List<Framework> frameworks = new List<Framework>();
            foreach (int i in devicePM.NewFrameworks)
                frameworks.Add(this.DeviceService.FrameworkById(i));

            devicePM.NewDevice.DeviceOS = oss;
            devicePM.NewDevice.DeviceFramework = frameworks;

            String imagePath = this.DeviceService.SaveImage(devicePM.ImageFile);
            devicePM.NewDevice.Image = imagePath;
            this.DeviceService.AddDevice(devicePM.NewDevice);

            return RedirectToAction("Index");
        }
    }
}