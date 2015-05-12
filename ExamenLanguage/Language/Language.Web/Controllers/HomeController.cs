using Language.BusinessLayer.Services;
using Language.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Language.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDeviceService DeviceServ = null; 

        public HomeController(IDeviceService deviceServ)
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
        public String GetCurrentCulture()
        {
            String message = "Culture: " + Thread.CurrentThread.CurrentCulture
                + " - UICulture: " + Thread.CurrentThread.CurrentUICulture;
            return message;
        }

        [HttpGet]
        public ActionResult Add()
        {
            Device device = new Device();
            return View(device);
        }
    }
}