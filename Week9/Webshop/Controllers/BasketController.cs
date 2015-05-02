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
        private IOrderService OrderServ = null;

        public BasketController(IBasketItemService basketItemServ, IApplicationUserService applicationUserServ, IDeviceService deviceServ, IOrderService orderServ)
        {
            this.BasketItemServ = basketItemServ;
            this.ApplicationUserServ = applicationUserServ;
            this.DeviceServ = deviceServ;
            this.OrderServ = orderServ;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Add(BasketItem basketItem)
        {
            if(User.Identity.IsAuthenticated)
            {
                Device device = this.DeviceServ.DeviceById(basketItem.NewDevice.ID);
                ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);

                basketItem.NewDevice = device;
                basketItem.NewUser = user;
                basketItem.Timestamp = DateTime.Now;
                basketItem.RentingPrice = device.RentingPrice;

                this.BasketItemServ.AddBasketItem(basketItem);
            }

            else if(!User.Identity.IsAuthenticated)
            {
                String visitorGUID;

                if(HttpContext.Request.Cookies["visitorGUID"] == null)
                {
                    visitorGUID = Guid.NewGuid().ToString();

                    HttpCookie cookie = new HttpCookie("visitorGUID");
                    cookie.Value = visitorGUID;
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.SetCookie(cookie);
                }

                else
                {
                    HttpCookie cookie = HttpContext.Request.Cookies["visitorGUID"];
                    visitorGUID = cookie.Value;
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.SetCookie(cookie);
                }

                Device device = this.DeviceServ.DeviceById(basketItem.NewDevice.ID);
                basketItem.NewDevice = device;
                basketItem.visitorGUID = visitorGUID;
                basketItem.Timestamp = DateTime.Now;
                basketItem.RentingPrice = device.RentingPrice;

                this.BasketItemServ.AddBasketItem(basketItem);
            }

            return RedirectToAction("Index", "Catalog");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            ApplicationUser user = new ApplicationUser();
            String visitorGUID = ""; 
            List<BasketItem> basketItems = new List<BasketItem>();

            if(User.Identity.IsAuthenticated)
            {
                user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
                basketItems = this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();
            }

            else if(HttpContext.Request.Cookies["visitorGUID"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["visitorGUID"];
                visitorGUID = cookie.Value;
                basketItems = this.BasketItemServ.BasketItemsByVisitorGUID(visitorGUID).ToList<BasketItem>();
            }

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

        [AllowAnonymous]
        [HttpGet]
        public int CountBasketItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
                return this.BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>().Count;
            }

            else
            {
                if(HttpContext.Request.Cookies["visitorGUID"] != null)
                {
                    HttpCookie cookie = HttpContext.Request.Cookies["visitorGUID"];
                    String visitorGUID = cookie.Value;
                    return this.BasketItemServ.BasketItemsByVisitorGUID(visitorGUID).ToList<BasketItem>().Count;
                }

                else
                {
                    return 0;
                }
            }
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            ApplicationUser user = ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            if(HttpContext.Request.Cookies["visitorGUID"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["visitorGUID"];
                String visitorGUID = cookie.Value;

                List<BasketItem> tempBasketItems = this.BasketItemServ.BasketItemsByVisitorGUID(visitorGUID).ToList<BasketItem>();
                foreach(BasketItem basketItem in tempBasketItems)
                {
                    basketItem.NewUser = user;
                    basketItem.visitorGUID = null;
                    this.BasketItemServ.UpdateBasketItemUser(basketItem);
                }
            }

            List<BasketItem> basketItems = BasketItemServ.BasketItemsByUser(user).ToList<BasketItem>();
            ApplicationUserBasketItemPM applicationUserBasketPM = new ApplicationUserBasketItemPM()
            {
                NewUser = user,
                NewBasketItems = basketItems,
                TotalPrice = new TotalPriceCalculator(basketItems).TotalPrice
            };

            return View(applicationUserBasketPM);
        }

        [HttpPost]
        public ActionResult Checkout(ApplicationUserBasketItemPM applicationUserBasketItemPM)
        {
            ApplicationUser tempUser = ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            tempUser.Name = applicationUserBasketItemPM.NewUser.Name;
            tempUser.Firstname = applicationUserBasketItemPM.NewUser.Firstname;
            tempUser.Address = applicationUserBasketItemPM.NewUser.Address;
            tempUser.City = applicationUserBasketItemPM.NewUser.City;
            tempUser.Zipcode = applicationUserBasketItemPM.NewUser.Zipcode;
            ApplicationUserServ.UpdateApplicationUser(tempUser);

            List<BasketItem> basketItems = BasketItemServ.BasketItemsByUser(tempUser).ToList<BasketItem>();
            ApplicationUser user = ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            double totalPrice = new TotalPriceCalculator(basketItems).TotalPrice;

            Order order = this.OrderServ.GenerateOrder(user, basketItems, totalPrice);
            this.OrderServ.AddOrder(order);

            foreach(BasketItem basketItem in basketItems)
            {
                this.BasketItemServ.DeleteBasketItem(basketItem.ID);
            }

            return RedirectToAction("Orders");
        }

        [HttpGet]
        public ActionResult Orders()
        {
            ApplicationUser user = ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
            List<Order> orders = OrderServ.OrdersByApplicationUser(user).ToList<Order>();
            return View(orders);
        }
    }
}