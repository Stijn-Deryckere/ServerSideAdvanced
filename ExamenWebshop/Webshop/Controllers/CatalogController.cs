using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.BusinessLayer.Services;
using Webshop.Models.Models;
using Webshop.Models.PresentationModels;

namespace Webshop.Controllers
{
    public class CatalogController : Controller
    {
        IDeviceService DeviceServ = null;

        public CatalogController(IDeviceService deviceServ)
        {
            this.DeviceServ = deviceServ;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = this.DeviceServ.AllDevices().ToList<Device>();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            BasketPM basketPM = new BasketPM()
            {
                NewDevice = this.DeviceServ.DeviceById(id),
                Amount = 0
            };

            return View(basketPM);
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<OS> os = this.DeviceServ.AllOSs().ToList<OS>();
            List<Framework> frameworks = this.DeviceServ.AllFrameworks().ToList<Framework>();
            Device device = new Device()
            {
                DeviceOSs = os,
                DeviceFrameworks = frameworks
            };

            DevicePM devicePM = new DevicePM()
            {
                NewDevice = device
            };
            return View(devicePM);
        }

        [HttpPost]
        public ActionResult Add(DevicePM devicePM)
        {
            if(ModelState.IsValid)
            {
                List<OS> oSs = new List<OS>();
                foreach(int id in devicePM.SelectedOSs)
                {
                    OS os = this.DeviceServ.OSById(id);
                    oSs.Add(os);
                }

                List<Framework> frameworks = new List<Framework>();
                foreach(int id in devicePM.SelectedFrameworks)
                {
                    Framework framework = this.DeviceServ.FrameworkById(id);
                    frameworks.Add(framework);
                }

                devicePM.NewDevice.DeviceOSs = oSs;
                devicePM.NewDevice.DeviceFrameworks = frameworks;
                devicePM.NewDevice.Picture = this.DeviceServ.SaveImage(devicePM.NewPicture);

                this.DeviceServ.AddDevice(devicePM.NewDevice);
                this.DeviceServ.RefreshCachedDevices();
            }

            return RedirectToAction("Index");
        }
    }
}