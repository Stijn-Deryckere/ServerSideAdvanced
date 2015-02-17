using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.ViewModels;

namespace Week2Oefening1.Controllers
{
    public class CatalogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = DeviceRepository.Get();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            Device device = DeviceRepository.Get(id);
            return View(device);
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public ActionResult Add()
        {
            List<Framework> fws = FrameworkRepository.Get();
            DeviceVM dvm = new DeviceVM()
            {
                NewDevice = new Device(),
                Frameworks = new SelectList(fws, "Id", "Name"),
                OperatingSystems = new SelectList()
            };
            return View();
        }
    }
}