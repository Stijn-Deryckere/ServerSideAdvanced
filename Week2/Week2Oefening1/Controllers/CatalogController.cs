using System;
using System.Collections.Generic;
using System.IO;
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
            List<OS> oss = OperatingSystemRepository.Get();

            DeviceVM dvm = new DeviceVM()
            {
                NewDevice = new Device(),
                Frameworks = new SelectList(fws, "Id", "Name"),
                OperatingSystems = new SelectList(oss, "Id", "Name")
            };
            return View(dvm);
        }

        [Authorize(Roles="Administrator")]
        [HttpPost]
        public ActionResult Add(DeviceVM dvm)
        {
            if (ModelState.IsValid)
            {
                if(dvm.ImageFile.ContentLength > 0)
                {
                    String fileName = Path.GetFileName(dvm.ImageFile.FileName);
                    String path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    dvm.ImageFile.SaveAs(path);
                    dvm.NewDevice.Image = fileName;
                }

                List<OS> operatingSystems = new List<OS>();
                foreach (int i in dvm.NewOperatingSystems)
                    operatingSystems.Add(OperatingSystemRepository.Get(i));

                List<Framework> frameworks = new List<Framework>();
                foreach (int i in dvm.NewFrameworks)
                    frameworks.Add(FrameworkRepository.Get(i));

                Device device = dvm.NewDevice;
                device.DeviceOS = operatingSystems;
                device.DeviceFramework = frameworks;

                DeviceRepository.Post(device);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }
    }
}