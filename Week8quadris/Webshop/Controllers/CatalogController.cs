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
            Device device = this.DeviceService.DeviceById(id);
            Boolean test = this.User.IsInRole("Administrator");
            return View(device);
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
    }
}