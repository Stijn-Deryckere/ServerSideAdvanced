using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.BusinessLayer.Calculations;
using Webshop.BusinessLayer.Services;
using Webshop.Models;
using Webshop.Models.PresentationModels;

namespace Webshop.Controllers
{
    [Authorize(Roles="Administrator, User")]
    public class BasketController : Controller
    {
        private IBasketItemService BasketItemServ = null;
        private IApplicationUserService ApplicationUserServ = null;
        private IDeviceService DeviceServ = null;

        public BasketController(IBasketItemService basketItemServ, IApplicationUserService applicationUserServ, IDeviceService deviceServ)
        {
            this.BasketItemServ = basketItemServ;
            this.ApplicationUserServ = applicationUserServ;
            this.DeviceServ = deviceServ;
        }

        [HttpPost]
        public ActionResult Add(BasketItem basketItem)
        {
            Device device = this.DeviceServ.DeviceById(basketItem.NewDevice.ID);
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);

            basketItem.NewDevice = device;
            basketItem.NewUser = user;
            basketItem.Timestamp = DateTime.Now;
            basketItem.RentingPrice = device.RentingPrice;

            this.BasketItemServ.AddBasketItem(basketItem);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        public ActionResult Index()
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            List<BasketItem> basketItems = this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();
            BasketItemTotalPricePM basketItemTotalPricePM = new BasketItemTotalPricePM();
            basketItemTotalPricePM.BasketItems = basketItems;
            basketItemTotalPricePM.TotalPrice = new TotalPriceCalculator(basketItems).TotalPrice;
            return View(basketItemTotalPricePM);
        }

        [HttpPost]
        public ActionResult Index(BasketItem basketItem)
        {
            BasketItem newBasketItem = this.BasketItemServ.BasketItemById(basketItem.ID);
            newBasketItem.Amount = basketItem.Amount;
            this.BasketItemServ.UpdateBasketItem(newBasketItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public int CountBasketItems()
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            return this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>().Count;
        }
    }
}