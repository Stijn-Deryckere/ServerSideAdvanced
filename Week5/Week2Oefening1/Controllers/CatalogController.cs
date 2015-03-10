using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.Models.Services;
using Week2Oefening1.ViewModels;

namespace Week2Oefening1.Controllers
{
    public class CatalogController : Controller
    {
        private IDeviceService devServ;
        private IBasketItemService basketItemServ;

        public CatalogController(IDeviceService ds, IBasketItemService bs)
        {
            this.devServ = ds;
            this.basketItemServ = bs;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = devServ.AllDevices().ToList<Device>();            
            return View(devices);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            BasketItemVM basketVM = new BasketItemVM()
            {
                NewBasketItem = new BasketItem(),
                NewDevice = devServ.DeviceById(id)
            };

            basketVM.NewBasketItem.RentDevice = basketVM.NewDevice;
            return View(basketVM);
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public ActionResult Add()
        {
            List<Framework> fws = devServ.AllFrameworks().ToList<Framework>();
            List<OS> oss = devServ.AllOSs().ToList<OS>();

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
                dvm.NewDevice.Image = dvm.ImageFile.FileName;
                devServ.AddImage(dvm.ImageFile);

                List<OS> operatingSystems = new List<OS>();
                foreach (int i in dvm.NewOperatingSystems)
                    operatingSystems.Add(devServ.OSById(i));

                List<Framework> frameworks = new List<Framework>();
                foreach (int i in dvm.NewFrameworks)
                    frameworks.Add(devServ.FrameworkById(i));

                Device device = dvm.NewDevice;
                device.DeviceOS = operatingSystems;
                device.DeviceFramework = frameworks;

                devServ.AddDevice(device);
                return RedirectToAction("Index");
            }

            else
                return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Rent(BasketItem basketItem)
        {
            Device device = devServ.DeviceById(basketItem.RentDevice.Id);
            basketItem.RentDevice = device;
            
            //Get user.
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            basketItem.RentUser = user;
            store.Context.Dispose();

            basketItem.Timestamp = DateTime.Now;
            basketItem.IsDeleted = false;

            basketItemServ.AddBasketItem(basketItem);
            return RedirectToAction("Index");
        }
    }
}