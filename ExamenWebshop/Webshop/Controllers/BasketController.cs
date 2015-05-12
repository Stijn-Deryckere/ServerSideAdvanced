using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.BusinessLayer.Calculators;
using Webshop.BusinessLayer.Services;
using Webshop.Models.Models;
using Webshop.Models.PresentationModels;

namespace Webshop.Controllers
{
    [Authorize(Roles="Administrator")]
    public class BasketController : Controller
    {
        private IApplicationUserService ApplicationUserServ = null;
        private IBasketItemService BasketItemServ = null;
        private IDeviceService DeviceServ = null;
        private IOrderService OrderServ = null;

        public BasketController(IApplicationUserService applicationUserServ, IBasketItemService basketItemServ, IDeviceService deviceServ, IOrderService orderServ)
        {
            this.ApplicationUserServ = applicationUserServ;
            this.BasketItemServ = basketItemServ;
            this.DeviceServ = deviceServ;
            this.OrderServ = orderServ;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            List<BasketItem> basketItems = this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();

            BasketItemsPM basketItemsPM = new BasketItemsPM()
            {
                BasketItems = basketItems,
                TotalPrice = new TotalPriceCalculator(basketItems).TotalPrice
            };

            return View(basketItemsPM);
        }

        [HttpPost]
        public ActionResult Add(BasketPM basketPM)
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            Device device = this.DeviceServ.DeviceById(basketPM.NewDevice.ID);
            BasketItem basketItem = this.BasketItemServ.CreateBasketItem(device, user, basketPM.Amount);

            this.BasketItemServ.AddBasketItem(basketItem);
            this.BasketItemServ.IncrementItemsCount(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public int CountBasketItems()
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            return this.BasketItemServ.CountItemsInBasket(user);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            List<BasketItem> basketItems = this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();

            CheckoutPM checkoutPM = new CheckoutPM()
            {
                NewUser = user,
                BasketItems = basketItems
            };

            return View(checkoutPM);
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutPM checkoutPM)
        {
            ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            user.Name = checkoutPM.NewUser.Name;
            user.Firstname = checkoutPM.NewUser.Firstname;
            user.Address = checkoutPM.NewUser.Address;
            user.Zipcode = checkoutPM.NewUser.Zipcode;
            user.City = checkoutPM.NewUser.City;
            this.ApplicationUserServ.UpdateApplicationUser(user);

            List<BasketItem> basketItems = this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();

            Order order = this.OrderServ.MakeOrder(basketItems, user);
            this.OrderServ.AddOrder(order);

            this.BasketItemServ.DeleteBasketItems(basketItems);
            this.BasketItemServ.RefreshItemsCount(user);

            return RedirectToAction("Index");
        }
    }
}